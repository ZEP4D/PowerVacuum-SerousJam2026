using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] Canvas credits_canvas;

    public void ToggleCredits()
    {
        this.credits_canvas.enabled = !this.credits_canvas.enabled;
    }
}
