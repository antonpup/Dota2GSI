namespace Dota2GSI.Nodes
{
    /// <summary>
    /// A class representing the authentication information for GSI
    /// </summary>
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
