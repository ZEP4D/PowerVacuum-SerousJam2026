using UnityEngine;

public class CalculateTurnOutput : MonoBehaviour
{
    [SerializeField] private Document document;
    public static CalculateTurnOutput instance;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        document = FindFirstObjectByType<Document>();
        Debug.Log(document.name);
    }

    public void CalculateTurn()
    {
        foreach (Decision decision in document.GetDecisions())
        {
            if (decision.GetStampState() == Decision.StampState.Approved)
            {
                switch (decision.GetTypeOfProject())
                {
                    case Decision.TypeOfProject.Build:
                    {
                        ResourcesSystem.instance.PayforConstrut(decision.GetPowerPlants(), 1);
                        break;
                    }
                    case Decision.TypeOfProject.Demolish:
                    {
                        ResourcesSystem.instance.RemovePlant(decision.GetPowerPlants());
                        break;
                    }
                    default: break;
                }

                for (int i = 0; i < decision.GetApprovalCosts().Count; i++)
                {
                    if (i > 5) break;
                    ResourcesSystem.instance.AffectResource((ResourcesSystem.ResourceType)i, decision.GetApprovalCosts()[i]);
                }
        }

            if (decision.GetStampState() == Decision.StampState.Disapproved)
            {
                for (int i = 0; i < decision.GetDisapprovalCosts().Count; i++)
                {
                    if (i > 5) break;
                    ResourcesSystem.instance.AffectResource((ResourcesSystem.ResourceType)i, decision.GetDisapprovalCosts()[i]);
                }
            }
        }
    }
    
    
}
