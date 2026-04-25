using UnityEngine;

public class StampMarkController : MonoBehaviour
{
    [field: Header("Sprites")]
    [SerializeField] private Sprite approveMark;
    [SerializeField] private Sprite rejectMark;

    public void SetStampMark(Decision.StampState? stampState)
    {
        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();

        switch (stampState ?? Decision.StampState.None) {
            case Decision.StampState.None:
                renderer.enabled = false;
            break;

            case Decision.StampState.Disapproved:
                renderer.enabled = true;
                renderer.sprite = rejectMark;
            break;

            case Decision.StampState.Approved:
                renderer.enabled = true;
                renderer.sprite = approveMark;
            break;
        }
    }
}
