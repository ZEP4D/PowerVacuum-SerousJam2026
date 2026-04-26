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
        foreach (var item in ResourcesSystem.instance.GetResources())
        {
            Debug.Log($"{item.Key}: {item.Value}");
        }
    }
}