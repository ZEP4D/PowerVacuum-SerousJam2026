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


    public void Howmuchusage()
    {
        foreach (PowerPlants_core pp in powerPlants)
        {
            if (!pp.GetisRenewable())
            {
                switch (pp.Gettypeofpowerp())
                {
                    case type.coal:
                        if(! (resources[ResourceType.Coal] <= 0))
                        {
                            resources[ResourceType.Coal] -= pp.GetresourceUsage();    
                        }
                        break;
                    case type.atomic:
                        if(! (resources[ResourceType.Uranium] <= 0))
                        {
                            resources[ResourceType.Uranium] -= pp.GetresourceUsage();    
                        }
                        break;
                    default:
                        break;
                }
            }
        }

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
            Howmuchusage();
            Calculatepolution();
            ApprovalCalulate();
            Howmuchenerygenerate();

            resources[ResourceType.Coal] += newCoal;
            resources[ResourceType.Uranium] += newUran;
            GetComponent<EndChecker>().CheckForWinCondition();
            numbersofturn++;
        }
        Debug.Log(numbersofturn);
    }    


    public void PayforConstrut(PowerPlants_core pp, int number)
    {
        int fullcost = number * pp.GetCost();
        while (fullcost < resources[ResourceType.Budget] && number > 0)
        { 
            AddnewPlant(pp);
            number--;
            fullcost -= pp.GetCost();
        }
    }


    public void deletemulitple(PowerPlants_core pp, int number)
    {
        if(number < powerPlants.Count)
        {
            for (int i = 0 ; i <= number; i++)
            {
                RemovePlant(pp);
            }
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
