using Dota2GSI.Nodes;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Auth update. 
    /// </summary>
    public class AuthStateUpdated : UpdateEvent<Auth>
    {
        public AuthStateUpdated(Auth new_value, Auth previous_value) : base(new_value, previous_value)
        {
        }
    }
}
