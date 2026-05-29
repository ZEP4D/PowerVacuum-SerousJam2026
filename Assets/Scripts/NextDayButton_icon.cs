using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NextDayButton_icon : MonoBehaviour
{
    [SerializeField] private Button button;
    
    void Start()
    {
        button.onClick.AddListener(EndTurn);
    }

    void EndTurn()
    {
        ResourcesSystem.instance.Endturnisup();
        CalculateTurnOutput_icon.instance.CalculateTurn();
        FindFirstObjectByType<Document_Icon>().loadDecisionList(ResourcesSystem.instance.getnumbersofturn());
        button.GameObject().SetActive(false);
    }
}