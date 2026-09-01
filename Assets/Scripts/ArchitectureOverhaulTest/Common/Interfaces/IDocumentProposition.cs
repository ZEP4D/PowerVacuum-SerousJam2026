using UnityEngine;

using System;
using System.Collections.Generic;


namespace ArchitectureOverhaul.Common
{
    public interface IDocumentProposition
    {
        // --== CLASS METHODS ==-- //
            public String GetDescription();
            public Dictionary<ResourceType, ResourceValuePair> GetResourcesToGain();
            public Dictionary<ResourceType, ResourceValuePair> GetResourcesToPay();

            public void SetState(StampState stampState);
            public void Finalise(IMCP mcp);
        // ==--
    }
}
