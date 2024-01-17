using Dota2GSI.Nodes;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Roshan update. 
    /// </summary>
    public class RoshanUpdated : UpdateEvent<Roshan>
    {
        public RoshanUpdated(Roshan new_value, Roshan previous_value) : base(new_value, previous_value)
        {
        }
    }
}
