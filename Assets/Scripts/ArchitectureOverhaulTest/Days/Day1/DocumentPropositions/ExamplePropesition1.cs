using UnityEngine;

using System;
using System.Collections.Generic;

using ArchitectureOverhaul.Common;

namespace ArchitectureOverhaul.DayControllers
{
    public class ExamplePropesition1 : MonoBehaviour, IDocumentProposition
    {
        // --== IDocumentProposition METHODS ==-- //
            public String GetDescription()
            {
                return new String("Example proposition");
            }

            public Dictionary<ResourceType, ResourceValuePair> GetResourcesToGain()
            {
                return new Dictionary<ResourceType, ResourceValuePair>();
            }
            public Dictionary<ResourceType, ResourceValuePair> GetResourcesToPay()
            {
                return new Dictionary<ResourceType, ResourceValuePair>();
            }

            public void SetState(StampState stampState)
            {

            }
            public void Finalise(IMCP mcp)
            {

            }
        // ==--
    }
}
