using System.Collections.Generic;
using UnityEngine;

public class ResourcesSystem : MonoBehaviour
{
    [SerializeField] private int approval;
    [SerializeField] private int climate;
    [SerializeField] private int energy;
    
    [SerializeField] private int budget;

    // Storage Attribute
    
    [SerializeField] private int coal;
    [SerializeField] private int uran;
    
    [SerializeField] private List<PowerPlants_core> powerPlants;


    // New Resource Attribute

    [SerializeField] private int newCoal;
    [SerializeField] private int newUran;



     void Update()
    {
          Debug.Log(
            "approval: " + approval + " " +
            "climate: " + climate + " " +
            "energy: " + energy + " " +
            "money: " + budget + " " +
            "coal: " + coal + " " +
            "uran: " + uran + " "  
        );
    }


    void Howmuchusage()
    {
        
        foreach(PowerPlants_core pp in powerPlants)
        {
            if (!pp.GetisRenewable())
            {
                switch (pp.Gettypeofpowerp())
                {
                    case type.coal:
                        coal -= pp.GetresourceUsage();
                        break;
                    case type.atomic:
                        uran -= pp.GetresourceUsage();
                        break;
                    default:
                        break;
                }
            }
        }

    }


    void Calculatepolution()
    {   
        climate = 0; 
        foreach(PowerPlants_core pp in powerPlants)
        {
            climate += pp.Getpolution(); 
        }
    }

    void ApprovalCalulate()
    {
        if(approval > 0)
        {
            foreach(PowerPlants_core pp in powerPlants)
            {
                approval += pp.Getliked();
            }
        }
    }

    void Howmuchenerygenerate()
    {
        energy = 0;

        foreach(PowerPlants_core pp in powerPlants)
        {
            energy += pp.GetEnergy();
        }

    }


    void AddnewPlant(PowerPlants_core powerp)
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
        }


        coal += newCoal;
        uran += newUran;
        
    }    


    public void PayforConstrut(PowerPlants_core pp, int number)
    {

        int fullcost = 0;
        for (int i = 0 ; i <= number; i++)
        {
            fullcost += pp.GetCost();
        }


        if(fullcost < budget)
        {
            for (int i = 0 ; i <= number; i++)
            {
                AddnewPlant(pp);
            }
        }
        else
        {
            return;
        }
    }


    public void deletemulitply(PowerPlants_core pp, int number)
    {
        if(number < powerPlants.Count)
        {
            for (int i = 0 ; i <= number; i++)
            {
                RemovePlant(pp);
            }
        }
        else
        {
            return;
        }
    }
}
