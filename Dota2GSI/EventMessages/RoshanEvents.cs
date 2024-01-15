using Dota2GSI.Nodes;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Roshan update. 
    /// </summary>
    public class RoshanStateUpdated : UpdateEvent<Roshan>
    {
        public RoshanStateUpdated(Roshan new_value, Roshan previous_value) : base(new_value, previous_value)
        {
        }
    }
}
