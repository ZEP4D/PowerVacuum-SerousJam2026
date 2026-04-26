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
    [SerializeField] private int newBudget = 3;
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


    public void Calculatepolution()
    {   
        resources[ResourceType.Climate] = 0; 
        foreach (PowerPlants_core pp in powerPlants)
        {
            resources[ResourceType.Climate] += pp.Getpolution(); 
        }
    }

    public void ApprovalCalulate()
    {
        foreach (PowerPlants_core pp in powerPlants)
            {
                resources[ResourceType.Approval] += pp.Getliked();
            }
        resources[ResourceType.Approval] = Mathf.Min(100, resources[ResourceType.Approval]);
    }

    public void Howmuchenerygenerate()
    {
        resources[ResourceType.Energy] = 0;

        foreach(PowerPlants_core pp in powerPlants)
        {
            if (!pp.GetisRenewable())
            {
                switch (pp.Gettypeofpowerp())
                {
                    case type.coal:
                    {
                        if (resources[ResourceType.Coal] >= pp.GetresourceUsage())
                        {
                            resources[ResourceType.Coal] -= pp.GetresourceUsage();
                            resources[ResourceType.Energy] += pp.GetEnergy();
                        }

                        break;
                    }
                    case type.atomic:
                    {
                        if (resources[ResourceType.Uranium] <= pp.GetresourceUsage())
                        {
                            resources[ResourceType.Uranium] -= pp.GetresourceUsage();
                            resources[ResourceType.Energy] += pp.GetEnergy();
                        }

                        break;
                    }
                    default:
                        break;
                }
                
            }
            resources[ResourceType.Energy] += pp.GetEnergy();
        }

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
        if (powerPlants.Count == 0)
        {
            Debug.Log("ERROR");
        }
        else
        {
            Calculatepolution();
            ApprovalCalulate();
            Howmuchenerygenerate();

            resources[ResourceType.Coal] += newCoal;
            resources[ResourceType.Uranium] += newUran;
            resources[ResourceType.Budget] += newBudget;
            GetComponent<EndChecker>().CheckForWinCondition();
            numbersofturn++;
            
        }
        Debug.Log(numbersofturn);
    }    


    public void PayforConstrut(List<PowerPlants_core> pp)
    {
        var fullcost = 0;
        foreach (PowerPlants_core plant in pp)
        {
            fullcost += plant.GetCost();
            if (fullcost > resources[ResourceType.Budget])
            {
                return;
            }
            AddnewPlant(plant);
        }
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
        resources[type] = Mathf.Min(100, resources[type] + value);
    }
}
