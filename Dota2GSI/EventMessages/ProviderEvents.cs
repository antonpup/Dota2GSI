using Dota2GSI.Nodes;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Provider update. 
    /// </summary>
    public class ProviderStateUpdated : UpdateEvent<Provider>
    {
        public ProviderStateUpdated(Provider new_value, Provider previous_value) : base(new_value, previous_value)
        {
        }
    }
}
