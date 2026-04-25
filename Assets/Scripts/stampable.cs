using Unity.VisualScripting;
using UnityEngine;

public class Stampable : MonoBehaviour
{
    [SerializeField] private Document document;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        document = GetComponentInParent<Document>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<StampDragAndDrop>() == null)
        {
            return;
        }
        if (other.gameObject.GetComponent<StampDragAndDrop>().currentStampleState == CurrentStampleState.Placed)
        {
            document.GetCurrentDecision()?.SetStampState(other.gameObject.GetComponent<StampDragAndDrop>().stampState);
            Debug.Log(document.GetCurrentDecision());
            Debug.Log(document.GetCurrentDecision()?.GetStampState());
        }
        
        
    }

}
