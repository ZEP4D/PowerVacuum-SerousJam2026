using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class StampSpaceController : MonoBehaviour, IStampInteractable
{
    // --== ATTACHED COMPONENTS ==-- //
        private SpriteRenderer spriteRenderer;
    // ==--


    // --== CLASS METHODS ==-- //
    // ==--


    // --== 'IStampInteractable' METHODS ==-- //
        public void SetHighlight(bool doHighlight)
        {
            this.spriteRenderer.color = doHighlight
                ? new Color(255, 255, 255, 255)
                : new Color(183, 255, 0,   255);
        }
    // ==--


    // --== UNITY METHODS ==-- //
        void Start()
        {
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
            this.spriteRenderer.color = new Color(255, 255, 0,   2550);
        }

        void Update()
        {
            
        }
    // ==--
}
