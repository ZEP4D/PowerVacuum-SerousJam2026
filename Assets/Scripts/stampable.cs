using UnityEngine;

public class Stampable : MonoBehaviour
{
    [SerializeField] private Document document;
    void OnMouseDown()
    {
        document.GetCurrentDecision()?.SetStampState(Decision.StampState.Approved);
        Debug.Log(document.GetCurrentDecision());
        Debug.Log(document.GetCurrentDecision()?.GetStampState());
    }

}
