using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Decision : MonoBehaviour
{
    public enum StampState
    {
        None, Approved, Disapproved
    }

    public enum TypeOfProject
    {
        Build, Demolish, Trade
    }
    
    [SerializeField] private StampState stampState = StampState.None;
    [SerializeField] private string description;
    [FormerlySerializedAs("pros")] [SerializeField] private string approvalGains;
    [FormerlySerializedAs("cons")] [SerializeField] private string denyGains;

    [field: Header("0 - approval, 1 - climate, 2 - energy, 3 - budget, 4 - coal, 5 - uranium\n6 - CountryA , 7 - CountryB,  8 - CountryC")] 
    [SerializeField] public List<int> approvalCosts;
    [SerializeField] public List<int> disapprovalCosts;
    [FormerlySerializedAs("powerPlant")] [SerializeField] public List<PowerPlants_core> powerPlants;
    [SerializeField] public TypeOfProject typeOfProject;
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
        return approvalGains;
    }
    
    public string GetCons()
    {
        return denyGains;
    }

    public List<PowerPlants_core> GetPowerPlants()
    {
        return powerPlants;
    }

    public TypeOfProject GetTypeOfProject()
    {
        return typeOfProject;
    }

    public List<int> GetApprovalCosts()
    {
        return approvalCosts;
    }
    
    public List<int> GetDisapprovalCosts()
    {
        return disapprovalCosts;
    }
}
