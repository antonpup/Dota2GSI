namespace Dota2GSI.Nodes
{
    public class Auth : Node
    {
        public readonly string Token;

        internal Auth(string json_data) : base(json_data)
        {
            Token = GetString("token");
        }
    }
}
