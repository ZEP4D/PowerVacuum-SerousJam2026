using UnityEngine;

public class SwapDecisions : MonoBehaviour
{
    [SerializeField] private Document document;
    [SerializeField] private int value;
    public void OnMouseDown()
    {
        document.ChangePage(value);
    }
}
