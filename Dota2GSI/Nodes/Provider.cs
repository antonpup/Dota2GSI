using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Information about the provider of this GameState.
    /// </summary>
    public class Provider : Node
    {
        /// <summary>
        /// Game name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Game's Steam AppID.
        /// </summary>
        public readonly int AppID;

        /// <summary>
        /// Game's version.
        /// </summary>
        public readonly int Version;

        /// <summary>
        /// Current timestamp.
        /// </summary>
        public readonly string TimeStamp;

        internal Provider(JObject parsed_data = null) : base(parsed_data)
        {
            Name = GetString("name");
            AppID = GetInt("appid");
            Version = GetInt("version");
            TimeStamp = GetString("timestamp");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Name: {Name}, " +
                $"AppID: {AppID}, " +
                $"Version: {Version}, " +
                $"TimeStamp: {TimeStamp}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Provider other &&
                Name.Equals(other.Name) &&
                AppID == other.AppID &&
                Version == other.Version &&
                TimeStamp.Equals(other.TimeStamp);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 464630987;
            hashCode = hashCode * -987549067 + Name.GetHashCode();
            hashCode = hashCode * -987549067 + AppID.GetHashCode();
            hashCode = hashCode * -987549067 + Version.GetHashCode();
            hashCode = hashCode * -987549067 + TimeStamp.GetHashCode();
            return hashCode;
        }
    }
}
