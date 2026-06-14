using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class StampMarkController : MonoBehaviour
{
    // --== ATTACHED COMPONENTS ==-- //
        private SpriteRenderer spriteRenderer;
    // ==--


    // --== CLASS FIELDS ==-- //
        public float lifetimeRemaining = 5f;
        public float fadeTime = 0.5f;
        private float fadeTimeRemaining;
    // ==--


    // --== UNITY METHODS ==-- //
        void Start() {
            this.fadeTimeRemaining = this.fadeTime;
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            this.lifetimeRemaining -= Time.deltaTime;
            if (this.lifetimeRemaining <= 0) {
                
                this.fadeTimeRemaining -= Time.deltaTime;
                if (this.fadeTimeRemaining > 0) {
                    Color spriteRendererColor = this.spriteRenderer.color;
                    spriteRendererColor.a = (this.fadeTimeRemaining / this.fadeTime);
                    this.spriteRenderer.color = spriteRendererColor;

                } else {
                    // If we faded, remove ourselves
                    Object.Destroy(this.gameObject);
                }

            } // if (this.lifetimeRemaining <= 0)
        }
    // ==--
    
}
