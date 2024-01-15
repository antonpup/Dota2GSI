using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes.LeagueProvider
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Stream other &&
                StreamID == other.StreamID &&
                Language == other.Language &&
                Name.Equals(other.Name) &&
                BroadcastProvider == other.BroadcastProvider &&
                StreamURL.Equals(other.StreamURL) &&
                VodURL.Equals(other.VodURL);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 192157661;
            hashCode = hashCode * -913935863 + StreamID.GetHashCode();
            hashCode = hashCode * -913935863 + Language.GetHashCode();
            hashCode = hashCode * -913935863 + Name.GetHashCode();
            hashCode = hashCode * -913935863 + BroadcastProvider.GetHashCode();
            hashCode = hashCode * -913935863 + StreamURL.GetHashCode();
            hashCode = hashCode * -913935863 + VodURL.GetHashCode();
            return hashCode;
        }
    }
}
