using DotNetTools.SharpGrabber.Internal;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetTools.SharpGrabber.Authentication
{
    public class HtmlFormBasicAuthenticator : IGrabberAuthenticator
    {
        #region Fields
        private readonly Uri _loginUri;
        #endregion

        #region Properties
        public string FormElementXPath { get; set; } = "//form";
        public string InputElementsXPath { get; set; } = "//input";
        public string UsernameFieldNameRegex { get; set; } = "(username|email)";
        public string PasswordFieldNameRegex { get; set; } = "(password|pw)";
        #endregion

        public HtmlFormBasicAuthenticator(Uri loginUri)
        {
            _loginUri = loginUri;
        }

        private async Task<HtmlDocument> FetchLoginPage(HttpClient client)
        {
            var response = await client.GetAsync(_loginUri).ConfigureAwait(false);

            using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                var doc = new HtmlDocument();
                doc.Load(responseStream);
                return doc;
            }
        }

        protected virtual bool FilterFormInput(string type, string name, string value)
        {
            return !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(name);
        }

        private IDictionary<string, string> MapInputValues(HtmlNodeCollection inputs)
        {
            const string NullString = null;
            var r = new Dictionary<string, string>();
            foreach (var input in inputs)
            {
                var type = input.GetAttributeValue("type", NullString);
                var name = input.GetAttributeValue("name", NullString);
                var value = input.GetAttributeValue("value", NullString);

                if (!FilterFormInput(type, name, value))
                    continue;

                r.Add(name, value);
            }
            return r;
        }

        private IDictionary<string, string> FillLoginForm(IDictionary<string, string> inputMap, ICredentials credentials)
        {
            var r = new Dictionary<string, string>();
            var userNameRegex = new Regex(UsernameFieldNameRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var passwordRegex = new Regex(PasswordFieldNameRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var basicCreds = credentials as NetworkCredential;

            foreach (var pair in inputMap)
            {
                var v = pair.Value;
                if (userNameRegex.IsMatch(pair.Key))
                    v = basicCreds?.UserName;
                else if (passwordRegex.IsMatch(pair.Key))
                    v = basicCreds?.Password;

                if (v != null)
                    r.Add(pair.Key, v);
            }
            return r;
        }

        public async Task AuthenticateAsync(ICredentials credentials)
        {
            var client = HttpHelper.CreateClient();
            var loginPage = await FetchLoginPage(client).ConfigureAwait(false);

            // process input form
            var formNode = loginPage.DocumentNode.SelectSingleNode(FormElementXPath) ?? throw new InvalidOperationException("Failed to find the form node.");
            var action = formNode.GetAttributeValue("action", null);
            var actionUri = action == null ? _loginUri : new Uri(_loginUri, action);
            var formMethod = new HttpMethod(formNode.GetAttributeValue("method", "POST"));
            var inputs = formNode.SelectNodes(InputElementsXPath);
            var inputMap = MapInputValues(inputs);

            // fill the form
            var formValues = FillLoginForm(inputMap, credentials);

            // send login request
            var request = new HttpRequestMessage(formMethod, actionUri);
            request.Headers.Referrer = _loginUri;
            if (formMethod.Method == "GET")
                throw new NotSupportedException($"GET forms are not supported by {GetType()} yet.");
            request.Content = new FormUrlEncodedContent(formValues);
            var response = await client.SendAsync(request).ConfigureAwait(false);
            using (var fs = new FileStream(@"D:\success.html", FileMode.Create))
            using (var s = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                s.CopyTo(fs);

            client.Dispose();
        }

        public void TouchHttpClient(HttpClient client)
        {
            throw new NotImplementedException();
        }
    }
}
