﻿namespace DotNetTools.SharpGrabber.YouTube
{
    /// <summary>
    /// Describes a YouTube stream.
    /// </summary>
    public class YouTubeStreamInfo
    {
        /// <summary>
        /// YouTube iTag number
        /// </summary>
        public int? iTag { get; set; }

        /// <summary>
        /// Type grabbed from metadata
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// File mime - extracted from <see cref="Type"/>
        /// </summary>
        public string Mime { get; set; }

        /// <summary>
        /// Optional suggested extension of the file. If this value is NULL, the extension can be implied from <see cref="Mime"/>.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Quality of the video e.g. 'medium'
        /// </summary>
        public string Quality { get; set; }

        /// <summary>
        /// Name of this quality suggested by YouTube e.g. mp4 720p
        /// </summary>
        public string QualityLabel { get; set; }

        /// <summary>
        /// Audio Sample Rate e.g. 48000, which stands for 48k (optional, audio only)
        /// </summary>
        public long AudioSampleRate { get; set; }

        /// <summary>
        /// Optional content length
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// Direct URL to the target file
        /// If deciphering is enabled, this property will correctly point to the secure resource.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Signature of the video, if any
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Calculated decipher
        /// </summary>
        public string Decipher { get; set; }


        /// <summary>
        /// Video frame size (video only)
        /// </summary>
        public RectSize FrameSize { get; set; }
    }
}