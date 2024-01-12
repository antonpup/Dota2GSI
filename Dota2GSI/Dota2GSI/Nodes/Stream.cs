using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// A class representing stream information.
    /// </summary>
    public class Stream : Node
    {
        /// <summary>
        /// The stream ID.
        /// </summary>
        public readonly int StreamID;

        /// <summary>
        /// The stream language.
        /// </summary>
        public readonly int Language;

        /// <summary>
        /// The stream name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The stream broadcast provider.
        /// </summary>
        public readonly int BroadcastProvider;

        /// <summary>
        /// The stream url.
        /// </summary>
        public readonly string StreamURL;

        /// <summary>
        /// The stream vod url.
        /// </summary>
        public readonly string VodURL;

        internal Stream(JObject parsed_data = null) : base(parsed_data)
        {
            StreamID = GetInt("stream_id");
            Language = GetInt("language");
            Name = GetString("name");
            BroadcastProvider = GetInt("broadcast_provider");
            StreamURL = GetString("stream_url");
            VodURL = GetString("vod_url");
        }

        public override string ToString()
        {
            return $"[" +
                $"StreamID: {StreamID}, " +
                $"Language: {Language}, " +
                $"Name: {Name}, " +
                $"BroadcastProvider: {BroadcastProvider}, " +
                $"StreamURL: {StreamURL}, " +
                $"VodURL: {VodURL}" +
                $"]";
        }
    }
}
