using System.Collections.Generic;
using UnityEngine;

public class Foreign_relations : MonoBehaviour
{
    
    [SerializeField] private List<Country_core> countries; 

    
    public int getrelation(Country_core core)
    {
        int index = Listfinder(core);
        return countries[index].getrelation();
    }


    public int getliked(Country_core core)
    {
        int index = Listfinder(core);
        return countries[index].getliked();
    }



    



    private int Listfinder(Country_core count)
    {
        for(int i = 0; i < countries.Count; i++)
        {
            if(countries[i] == count)
            {
                return i;
            }
        }

        return 0;
    }

}
