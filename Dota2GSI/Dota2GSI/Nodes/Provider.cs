namespace Dota2GSI.Nodes
{
    public class Provider : Node
    {
        public readonly string Name;
        public readonly int AppID;
        public readonly int Version;
        public readonly string TimeStamp;

        internal Provider(string json_data) : base(json_data)
        {
            Name = GetString("name");
            AppID = GetInt("appid");
            Version = GetInt("version");
            TimeStamp = GetString("timestamp");
        }
    }
}
