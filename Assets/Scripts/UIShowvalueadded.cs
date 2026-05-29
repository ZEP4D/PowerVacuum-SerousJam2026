using TMPro;
using UnityEngine;

public class UIShowvalueadded : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private ResourcesSystem.ResourceType type;
    private CalculateTurnOutput_icon turnOutput;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var showValue = CalculateTurnOutput_icon.instance.CalculateDecisionCosts()[type];
        switch (type)
        {
            case ResourcesSystem.ResourceType.Approval:
                showValue += ResourcesSystem.instance.CalculatePassiveApproval();
                break;
            case  ResourcesSystem.ResourceType.Climate:
                showValue += ResourcesSystem.instance.CalculatePassivePolution();
                break;
            case ResourcesSystem.ResourceType.Energy:
                showValue += ResourcesSystem.instance.CalculatePassiveEnergy(); 
                break;
            case ResourcesSystem.ResourceType.Coal:
                showValue -= ResourcesSystem.instance.CalculateCoalUsage();
                break;
            default:
                break;
        }
        text.text = Format(showValue);
    }
    
    private string Format(int value)
    {
        string messege = "";
        if(value > 0)
        {
            messege = $"(+{value})";
        }
        else
        {
             messege = $"({value})";   
        }   

        return messege;
    }
}
