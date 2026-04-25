using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class Decision : MonoBehaviour
{
    public enum StampState
    {
        None, Approved, Disapproved
    }

    public enum TypeOfProject
    {
        Build, Demolish, Replace
    }
    
    [SerializeField] private StampState stampState = StampState.None;
    [SerializeField] private string description;
    [SerializeField] private string pros;
    [SerializeField] private string cons;
    [SerializeField] private string cost;

    [field: Header("0 - approval, 1 - climate, 2 - energy, 3 - budget, 4 - coal, 5 - uranium ")] [SerializeField]
    public List<int> approvalCosts;

    [SerializeField] public List<int> disapprovalCosts;
    [SerializeField] public PowerPlants_core powerPlant;
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
        return pros;
    }
    
    public string GetCons()
    {
        return cons;
    }
    
    public string GetCost()
    {
        return cost;
    }
}
