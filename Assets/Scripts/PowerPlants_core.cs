using UnityEngine;


public enum  type{ coal, wind, water, atomic, solar, gas}
[CreateAssetMenu(fileName = "PowerPlants_core", menuName = "Scriptable Objects/PowerPlants_core", order = 2)] 
public class PowerPlants_core : ScriptableObject
{
    [SerializeField] private string description;
    [SerializeField] private bool isRenewable;
    [SerializeField] private type type_pp;
    [SerializeField] private int cost;
    [SerializeField] private int liked;
    [SerializeField] private int generateofpower;
    [SerializeField] private int polution;
    [SerializeField] private int resoureceUsage;
    [SerializeField] private Mesh Object;


    void Awake()
    {
        if(type_pp == type.wind || type_pp == type.solar || type_pp == type.water)
        {
            isRenewable = true;
        }
        else
        {
            isRenewable = false;            
        }
    }


    public bool GetisRenewable()
    {
        return isRenewable;
    }

    public int GetresourceUsage()
    {
        return resoureceUsage;
    }

    public int Getpolution()
    {
        return polution;
    }

    public type Gettypeofpowerp()
    {
        return type_pp;
    }

    public int Getliked()
    {
        return liked;
    }

    public int GetEnergy()
    {
        return generateofpower;
    }
}   
