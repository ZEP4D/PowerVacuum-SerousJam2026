using UnityEngine;


namespace ArchitectureOverhaul
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class StampAreaController : MonoBehaviour, IStampInteractable
    {
        // --== ATTACHED COMPONENTS ==-- //
            private SpriteRenderer spriteRenderer;
        // ==--


        // --== SERIALIZED FIELDS ==-- //
            [SerializeField] MasterControlProgram m_masterControlProgram;
            
            [SerializeField] Sprite m_approvingSprite;
            [SerializeField] Sprite m_declingSprite;
        // ==--


        // --== CLASS METHODS ==-- //
        // ==--


        // --== 'IStampInteractable' METHODS ==-- //
            public void SetHighlight(bool doHighlight, StampController withStamp)
            {
                Sprite sprite = withStamp.stampType == StampType.Approving 
                    ? this.m_approvingSprite
                    : this.m_declingSprite;

                this.spriteRenderer.enabled = doHighlight;
                this.spriteRenderer.sprite = sprite;
            }
            public void InteractPrimary(StampController withStamp)
            {
                //
            }
        // ==--


        // --== UNITY METHODS ==-- //
            void Start()
            {
                this.spriteRenderer = this.GetComponent<SpriteRenderer>();
            }

            void Update()
            {
                
            }
        // ==--
    }
}
