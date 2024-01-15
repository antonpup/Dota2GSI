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

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Token: {Token}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Auth other &&
                Token.Equals(other.Token);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 930327133;
            hashCode = hashCode * -487188821 + Token.GetHashCode();
            return hashCode;
        }
    }
}
