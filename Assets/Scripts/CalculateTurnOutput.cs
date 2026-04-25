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
                switch (decision.typeOfProject)
                {
                    case Decision.TypeOfProject.Build:
                    {
                        ResourcesSystem.instance.PayforConstrut(decision.powerPlant, 1);
                        break;
                    }
                    case Decision.TypeOfProject.Demolish:
                    {
                        ResourcesSystem.instance.RemovePlant(decision.powerPlant);
                        break;
                    }
                    default: break;
                }

                for (int i = 0; i < decision.approvalCosts.Count; i++)
                {
                    ResourcesSystem.instance.AffectResource((ResourcesSystem.ResourceType)i, decision.approvalCosts[i]);
                }
        }

            if (decision.GetStampState() == Decision.StampState.Disapproved)
            {
                for (int i = 0; i < decision.disapprovalCosts.Count; i++)
                {
                    ResourcesSystem.instance.AffectResource((ResourcesSystem.ResourceType)i, decision.disapprovalCosts[i]);
                }
            }
        }
    }
    
    
}
