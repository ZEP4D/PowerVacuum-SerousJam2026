using UnityEngine;

public class Stampable_icon : MonoBehaviour
{
    [SerializeField] private Document_Icon document;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite stampSpriteDisapprove;
    [SerializeField] private Sprite stampSpriteApprove;

    void Start()
    {
        document = GetComponentInParent<Document_Icon>();
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
            var state = other.gameObject.GetComponent<StampDragAndDrop>().stampState;
            document.GetCurrentDecision().SetStampState(state);
            switch (state)
            {
                case Decision.StampState.Approved:
                    spriteRenderer.sprite = stampSpriteApprove;
                    break;
                case Decision.StampState.Disapproved:
                    spriteRenderer.sprite = stampSpriteDisapprove;
                    break;
                default:
                    break;
            }
            document.SetCurrentDecision(document.GetCurrentDecision());
        }
    }

    public void showStamp(Decision.StampState state)
    {
        switch (state)
        {
            case Decision.StampState.Approved:
                spriteRenderer.sprite = stampSpriteApprove;
                break;
            case Decision.StampState.Disapproved:
                spriteRenderer.sprite = stampSpriteDisapprove;
                break;
            default:
                spriteRenderer.sprite = null;
                break;
        }
    }

}
