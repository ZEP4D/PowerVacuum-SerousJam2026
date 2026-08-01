using UnityEngine;

using System;



using ArchitectureOverhaul.Common;
namespace ArchitectureOverhaul
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class StampAreaController : MonoBehaviour, IStampInteractable
    {
        // --== ATTACHED COMPONENTS ==-- //
            private IMCP m_masterControlProgram;
        // ==--


        // --== SERIALIZED FIELDS ==-- //
            [SerializeField] GameObject _masterControlProgram;
            
            [SerializeField] Sprite m_approvingSprite;
            [SerializeField] Sprite m_declingSprite;
        // ==--


        // --== CLASS FIELDS ==-- //
            private SpriteRenderer m_spriteRenderer;
        // ==--


        // --== CLASS METHODS ==-- //
        // ==--


        // --== 'IStampInteractable' METHODS ==-- //
            public void SetHighlight(bool doHighlight, IStampController withStamp)
            {
                Sprite sprite = withStamp.GetStampType() == StampType.Approving 
                    ? this.m_approvingSprite
                    : this.m_declingSprite;

                this.m_spriteRenderer.enabled = doHighlight;
                this.m_spriteRenderer.sprite = sprite;
            }
            public void InteractPrimary(IStampController withStamp)
            {
                this.m_masterControlProgram.DocumentStampAreaInteracted(withStamp);
            }
        // ==--


        // --== UNITY METHODS ==-- //
            void Start()
            {
                this.m_spriteRenderer = this.GetComponent<SpriteRenderer>();

                // Verify 'this._masterControlProgram' is an instance of 'IMCP'
                #nullable enable
                IMCP? candidateMCP = this._masterControlProgram.GetComponent<IMCP>();
                if (candidateMCP is not null ) this.m_masterControlProgram = candidateMCP;
                else throw new Exception("Provided 'MasterControllProgram' does not implement interface 'IMCP'. Aborting");
            }

            void Update()
            {
                
            }
        // ==--
    }
}
