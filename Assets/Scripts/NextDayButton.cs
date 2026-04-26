using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NextDayButton : MonoBehaviour
{
    [SerializeField] private Button button;
    
    void Start()
    {
        button.onClick.AddListener(EndTurn);
    }

    void EndTurn()
    {
        ResourcesSystem.instance.Endturnisup();
        CalculateTurnOutput.instance.CalculateTurn();
        FindFirstObjectByType<Document>().loadDecisionList(ResourcesSystem.instance.getnumbersofturn());
        button.GameObject().SetActive(false);
    }
}