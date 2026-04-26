using System.Collections.Generic;
using UnityEngine;



public enum StampState { None, Approved, Disapproved }
public enum TypeOfProject { Build, Demolish, Trade }


[CreateAssetMenu(fileName = "Decision_Core", menuName = "Scriptable Objects/Decision_Core")]
public class Decision_Core : ScriptableObject
{   
     [SerializeField] private StampState stampState = StampState.None;
    [SerializeField] private string description;
    [SerializeField] private string approvalgain;
    [SerializeField] private string lostgain;

    [field: Header("0 - approval, 1 - climate, 2 - energy, 3 - budget, 4 - coal, 5 - uranium\n6 - CountryA , 7- CountryB,  8-CountryC ")] 
    [SerializeField] private List<int> approvalCost;
    [SerializeField] private List<int> disapprovalCosts;

    [SerializeField] private PowerPlants_core powerplants;
    [SerializeField] private TypeOfProject typeOfProject;
    [SerializeField] private Country_core CountryA;
    [SerializeField] private Country_core CountryB;
    [SerializeField] private Country_core CountryC;    

     public bool IsStamped()
    {
        return stampState != StampState.None;
    }

    public StampState GetStampState()
    {
        return stampState;
    }
    public void SetStampState(StampState state)
    {
        stampState = state;
    }

    public string GetDescription()
    {
        return description;
    }

    public string GetPros()
    {
        return approvalgain;
    }
    
    public string GetCons()
    {
        return lostgain;
    }

    public Country_core GetCountryA()
    {
        return CountryA;
    }

    public Country_core GetCountryB()
    {
        return CountryA;
    }

    public Country_core GetCountryC()
    {
        return CountryA;
    }
}   
