using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Foreign_relations : MonoBehaviour
{
    
    [SerializeField] private List<Countries> countries;



    void Update()
    {
        foreach(Countries core in countries)
        {
            Debug.Log(core.GetCountry());
        }
    }

    public int getrelation(Countries core)
    {
        return countries[Listfinder(core)].getrelation();
    }


    public int getliked(Countries core)
    { 
        return countries[Listfinder(core)].getliked();
    }


    public void changeRelation(Countries core, int para)
    {
        countries[Listfinder(core)].changeRelation(para);
    }

    public void changeLiked(Countries core, int para)
    {
        countries[Listfinder(core)].changeLiked(para);
    }


    public bool CountrywantTrade(Countries core)
    {
        if(countries[Listfinder(core)].getrelation() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }    

    private int Listfinder(Countries count)
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
