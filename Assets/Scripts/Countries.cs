using UnityEngine;
using System.Collections.Generic;

public class Countries : MonoBehaviour
{
    private string Country_name;
    private string ISO_Code;
    private int relation; 
    private int Liked;
    [SerializeField] private Country_core core;
    [SerializeField] private List<type> resourcesexport;



    void Start()
    {
        Country_name = core.GetCountry();
        ISO_Code = core.GetISO();
        relation = core.getrelation();
        Liked = core.getliked();
    }


    public string GetCountry()
    {
        return Country_name;
    }

    public string GetISO()
    {
        return ISO_Code;
    }
    public int getrelation()
    {
        return relation;
    }

    public int getliked()
    {
        return Liked;
    }

    public List<type> getresourcestype()
    {
        return resourcesexport;
    }


    public void changeRelation(int para)
    {
        relation += para;
    }

    public void changeLiked(int para)
    {
        Liked += para;
    }
}
