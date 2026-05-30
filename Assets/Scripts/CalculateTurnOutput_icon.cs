using System.Collections.Generic;
using UnityEngine;

public class CalculateTurnOutput_icon : MonoBehaviour
{
    [SerializeField] private Document_Icon document;
    public static CalculateTurnOutput_icon instance;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        document = FindFirstObjectByType<Document_Icon>();
        Debug.Log(document.name);
    }

    public void ApplyDecisionCosts()
    {
        foreach (Decision decision in document.GetDecisions())
        {
            if (decision.GetStampState() == Decision.StampState.Approved)
            {
                for (int i = 0; i < decision.GetApprovalCosts().Count; i++)
                {
                    if (i > 4) break;
                    ResourcesSystem.instance.AffectResource((ResourcesSystem.ResourceType)i, decision.GetApprovalCosts()[i]);
                }
        }

            if (decision.GetStampState() == Decision.StampState.Disapproved)
            {
                for (int i = 0; i < decision.GetDisapprovalCosts().Count; i++)
                {
                    if (i > 4) break;
                    ResourcesSystem.instance.AffectResource((ResourcesSystem.ResourceType)i, decision.GetDisapprovalCosts()[i]);
                }
            }
        }
    }
    
    public Dictionary<ResourcesSystem.ResourceType, int> CalculateDecisionCosts()
    {
        Dictionary<ResourcesSystem.ResourceType, int> decisionCosts = new ();
        decisionCosts.Add(ResourcesSystem.ResourceType.Approval, 0);
        decisionCosts.Add(ResourcesSystem.ResourceType.Climate, 0);
        decisionCosts.Add(ResourcesSystem.ResourceType.Energy, 0);
        decisionCosts.Add(ResourcesSystem.ResourceType.Budget, 0);
        decisionCosts.Add(ResourcesSystem.ResourceType.Coal, 0);
        
        foreach (Decision decision in document.GetDecisions())
        {
            if (decision.GetStampState() == Decision.StampState.Approved)
            {
                for (int i = 0; i < decision.GetApprovalCosts().Count; i++)
                {
                    if (i >= decisionCosts.Count) break;
                    decisionCosts[(ResourcesSystem.ResourceType)i] += decision.GetApprovalCosts()[i];
                }
                
                if (decision.typeOfProject == Decision.TypeOfProject.Build)
                {
                    foreach (PowerPlants_core pp in decision.GetPowerPlants())
                    {
                        decisionCosts[ResourcesSystem.ResourceType.Budget] -= pp.GetCost();
                        decisionCosts[ResourcesSystem.ResourceType.Approval] += pp.Getliked();
                        decisionCosts[ResourcesSystem.ResourceType.Climate] += pp.Getpolution();
                    }
                }
            }

            if (decision.GetStampState() == Decision.StampState.Disapproved)
            {
                for (int i = 0; i < decision.GetDisapprovalCosts().Count; i++)
                {
                    if (i >= decisionCosts.Count) break;
                    decisionCosts[(ResourcesSystem.ResourceType)i] += decision.GetDisapprovalCosts()[i];
                }
            }
        }

        return decisionCosts;
    }

    public void Construction()
    {
        foreach (Decision decision in document.GetDecisions())
        {
            switch (decision.GetTypeOfProject())
            {
                case Decision.TypeOfProject.Build:
                {
                    if (decision.GetStampState() == Decision.StampState.Approved) 
                        ResourcesSystem.instance.BuildPowerPlants(decision.GetPowerPlants());
                    break;
                }
                case Decision.TypeOfProject.Demolish:
                {
                    ResourcesSystem.instance.deletemulitple(decision.GetPowerPlants());
                    break;
                }
                default: break;
            }
        }
    }
    
    
}
