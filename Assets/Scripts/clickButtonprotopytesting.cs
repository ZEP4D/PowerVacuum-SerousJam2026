using UnityEngine;
using UnityEngine.UI;

public class clickButtonprotopytesting : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ResourcesSystem resources;


    void Start()
    {
        button.onClick.AddListener(EndTurn);
    }


    void EndTurn()
    {
        resources.Endturnisup();
    }
}
