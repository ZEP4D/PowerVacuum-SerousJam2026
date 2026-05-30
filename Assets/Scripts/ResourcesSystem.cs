using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class ResourcesSystem : MonoBehaviour
{
    public enum ResourceType
    {
        Approval, Climate, Energy, Budget, Coal, Uranium
    }

    public static ResourcesSystem instance;
    [SerializeField] private int startingApproval;
    [SerializeField] private int startingClimate;
    [SerializeField] private int startingEnergy;
    
    [SerializeField] private int startingBudget;

    // Storage Attribute
    
    [SerializeField] private int startingCoal;
    [SerializeField] private int startingUranium;
    private Dictionary<ResourceType, int> resources = new();
    [SerializeField] private List<PowerPlants_core> powerPlants;


    // New Resource Attribute

    [SerializeField] private int newCoal;
    [SerializeField] private int newUran;
    [SerializeField] private int newBudget;
    private int numbersofturn;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        numbersofturn = 0;
        resources.Add(ResourceType.Approval, startingApproval);
        resources.Add(ResourceType.Climate, startingClimate);
        resources.Add(ResourceType.Energy, startingEnergy);
        resources.Add(ResourceType.Budget, startingBudget);
        resources.Add(ResourceType.Coal, startingCoal);
        resources.Add(ResourceType.Uranium, startingUranium);
    }
    
    public int CalculatePassivePolution()
    {
        int polution = 0;
        foreach (PowerPlants_core pp in powerPlants)
        {
            polution += pp.Getpolution(); 
        }

        return polution;
    }

    public int CalculatePassiveApproval()
    {
        int approval = 0;
        foreach (PowerPlants_core pp in powerPlants)
            {
                approval += pp.Getliked();
            }
        return approval;
    }

    public int CalculatePassiveEnergy()
    {
        int energy = 0;
        int currentCoalUsage = 0;
        foreach(PowerPlants_core pp in powerPlants)
        {
            if (!pp.GetisRenewable())
            {
                switch (pp.Gettypeofpowerp())
                {
                    case type.coal:
                    {
                        if (currentCoalUsage <= resources[ResourceType.Coal] - pp.GetresourceUsage())
                        {
                            currentCoalUsage += pp.GetresourceUsage();
                            energy += pp.GetEnergy();
                        }
                        break;
                    }
                    default: break;
                }
                
            }
            else
            {
                energy += pp.GetEnergy();
            }
        }

        return energy;
    }

    public int CalculateCoalUsage()
    {
        int currentCoalUsage = 0;
        foreach (PowerPlants_core pp in powerPlants)
        {
            if (!pp.GetisRenewable())
            {
                switch (pp.Gettypeofpowerp())
                {
                    case type.coal:
                    {
                        if (currentCoalUsage <= resources[ResourceType.Coal] - pp.GetresourceUsage())
                        {
                            currentCoalUsage += pp.GetresourceUsage();
                        }

                        break;
                    }
                    default: break;
                }
            }
        }
        return currentCoalUsage;
    }
    
    public void AddnewPlant(PowerPlants_core powerp)
    {
        powerPlants.Add(powerp);        
    }

    public void RemovePlant(PowerPlants_core plant)
    {
        powerPlants.Remove(plant);
    }


    public void Endturnisup()
    {
        CalculateTurnOutput_icon.instance.ApplyDecisionCosts();
        AffectResource(ResourceType.Climate, CalculatePassivePolution());
        AffectResource(ResourceType.Approval, CalculatePassiveApproval());
        AffectResource(ResourceType.Energy, CalculatePassiveEnergy());
        AffectResource(ResourceType.Coal, -CalculateCoalUsage());
        CalculateTurnOutput_icon.instance.Construction();
        GetComponent<EndChecker>().CheckForWinCondition();
        for (int i = 0; i < 6; i++)
        {
            TrimResources((ResourceType)i);
        }
        numbersofturn++;
        Debug.Log(numbersofturn);
    }    


    public void BuildPowerPlants(List<PowerPlants_core> pp)
    {
        var fullcost = 0;
        var approval = 0;
        var polution = 0;
        foreach (PowerPlants_core plant in pp)
        {
            fullcost += plant.GetCost();
            approval += plant.Getliked();
            polution += plant.Getpolution();
            AddnewPlant(plant);
        }
        resources[ResourceType.Budget] -= fullcost;
        resources[ResourceType.Approval] += approval;
        resources[ResourceType.Climate] += polution;
    }


    public void deletemulitple(List<PowerPlants_core> pp)
    {
        for (int i = 0 ; i < pp.Count; i++)
            {
                RemovePlant(pp[i]);
            }
    }


    public int getApproval()
    {
        return resources[ResourceType.Approval];
    }

    public int getbudget()
    {
        return resources[ResourceType.Budget];
    }

    public int getclimate()
    {
        return resources[ResourceType.Climate];
    }


    public int getnumbersofturn()
    {
        return numbersofturn;
    }

    public Dictionary<ResourceType, int> GetResources()
    {
        return resources;
    }

    public void AffectResource(ResourceType type, int value)
    {
        resources[type] += value;
    }

    public void TrimResources(ResourceType type)
    {
        resources[type] = Mathf.Clamp(resources[type], 0, 100);
    }

    public List<PowerPlants_core> GetPowerPlants_s()
    {
        return powerPlants;
    }
}
