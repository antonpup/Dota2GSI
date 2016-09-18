namespace Dota2GSI.Nodes
{
    public class Auth : Node
    {
        /// <summary>
        /// The auth token sent by this GSI
        /// </summary>
        public readonly string Token;

        internal Auth(string json_data) : base(json_data)
        {
            Token = GetString("token");
        }
    }
}
