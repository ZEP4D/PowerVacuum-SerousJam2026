using System.Collections.Generic;
using UnityEngine;

public class ResourceFinder
{
    List<Country_core> FindResourcesInCountries(
        List<type> expectedResources,
        List<Country_core> scannableCountries
    ) {
        List<Country_core> suitableCountries = new List<Country_core>();


        foreach (Country_core candidateCountry in scannableCountries) {
            bool meetsRequirements = true;

            foreach ( type resource in expectedResources ) {
                if ( !candidateCountry.GetResourceExports().Contains(resource) ) {
                    meetsRequirements = false;
                }
            }

            if (meetsRequirements) suitableCountries.Add(candidateCountry);
        }


        return scannableCountries;
    }
}
