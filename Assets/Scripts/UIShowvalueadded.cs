using TMPro;
using UnityEngine;

public class UIShowvalueadded : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private ResourcesSystem.ResourceType type;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        switch (type)
        {
            case ResourcesSystem.ResourceType.Approval:
                    text.text = Format(25); 
                    break;
            case  ResourcesSystem.ResourceType.Climate:
                    text.text = Format(25);
                    break;
            case ResourcesSystem.ResourceType.Energy:
                    text.text = Format(-2); 
                    break;
            case ResourcesSystem.ResourceType.Budget:
                    text.text = Format(15);
                    break;
            case ResourcesSystem.ResourceType.Coal:
                    text.text = Format(19);
                    break;
        }
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
