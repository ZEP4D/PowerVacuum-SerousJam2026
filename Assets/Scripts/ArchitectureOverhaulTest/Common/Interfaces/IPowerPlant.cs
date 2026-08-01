#nullable enable

using UnityEngine;

using System.Collections.Generic;


namespace ArchitectureOverhaul.Common
{
    public interface IPowerPlant
    {
        PowerPlantState GetState();
        IDocumentProposition? GetBuildProposition();

        Dictionary<ResourceType, ResourceValuePair> GetResourcesProduced();
        Dictionary<ResourceType, ResourceValuePair> GetResourcesConsumed();
    }
}
