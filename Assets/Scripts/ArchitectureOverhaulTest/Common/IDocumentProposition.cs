using UnityEngine;

using System.Collections.Generic;


namespace ArchitectureOverhaul.Common
{
    public interface IDocumentProposition
    {
        // --== CLASS METHODS ==-- //
        public Dictionary<ResourceType, ResourceValuePair> GetResourcesToGain()
        {
            return new();    
        }


        public Dictionary<ResourceType, ResourceValuePair> GetResourcesToPay()
        {
            return new();
        }


        public void SetState(StampState stampState) {}


        public void Finalise(IMCP mcp) {}
        // ==--
    }
}
