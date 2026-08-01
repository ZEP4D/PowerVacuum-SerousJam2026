#nullable enable

using UnityEngine;

using System.Collections.Generic;

using ArchitectureOverhaul.Common;
namespace ArchitectureOverhaul.PowerPlants
{
    public class CoalPowerPlant : IPowerPlant
    {
        // --== CLASS FIELDS ==-- //
        // ==--


        // --== CLASS CONSTRUCTOR ==-- //
            public CoalPowerPlant()
            {
                //
            }
        // ==--


        // --== 'IPowerPlant' METHODS ==-- //
            public PowerPlantState GetState()
            {
                return PowerPlantState.Complete;
            }


            public IDocumentProposition? GetBuildProposition()
            {
                return null;
            }


            public Dictionary<ResourceType, ResourceValuePair> GetResourcesProduced()
            {
                return new();
            }
            

            public Dictionary<ResourceType, ResourceValuePair> GetResourcesConsumed()
            {
                return new();
            }
        // ==--
    }
}
