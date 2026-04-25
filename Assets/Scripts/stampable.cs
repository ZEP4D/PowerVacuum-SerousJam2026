using Unity.VisualScripting;
using UnityEngine;

public class Stampable : MonoBehaviour
{
    [SerializeField] private Document document;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private StampMarkController stampMarkController;
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
            Decision.StampState stampState = other.gameObject.GetComponent<StampDragAndDrop>().stampState;

            document.GetCurrentDecision()?.SetStampState(stampState);
            //stampMarkController.SetStampMark(stampState);
            Debug.Log(
                "Stampable >> Setting state to: " + other
                 .gameObject
                 .name
            );
            Debug.Log("RAGH");
            
            Decision.StampState stampType;
            switch (other.gameObject.name) {
                case "Approve":
                    stampType = Decision.StampState.Approved;
                break;

                case "Reject":
                    stampType = Decision.StampState.Disapproved;
                break;

                default:
                    stampType = Decision.StampState.None;
                break;
            }

            stampMarkController.SetStampMark(stampType);

        }
        
        
    }

}
