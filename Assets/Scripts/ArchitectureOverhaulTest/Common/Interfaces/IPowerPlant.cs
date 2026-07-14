#nullable enable

using UnityEngine;

namespace ArchitectureOverhaul.Common
{
    public interface IPowerPlant
    {
        PowerPlantState GetState();
        IDocumentProposition? GetBuildProposition();
    }
}
