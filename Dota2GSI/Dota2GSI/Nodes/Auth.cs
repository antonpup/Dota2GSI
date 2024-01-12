using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// A class representing the authentication information for GSI.
    /// </summary>
    public class Auth : Node
    {
        /// <summary>
        /// The auth token sent by this GSI.
        /// </summary>
        public readonly string Token;

        internal Auth(JObject parsed_data = null) : base(parsed_data)
        {
            Token = GetString("token");
        }

        public override string ToString()
        {
            return $"[" +
                $"Token: {Token}" +
                $"]";
        }
    }
}
