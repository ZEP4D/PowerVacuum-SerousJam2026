using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using System;
using System.Text;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class StampController : MonoBehaviour, IPointerClickHandler
{
    // --== SERIALIZED FIELDS ==-- //
        [SerializeField] StampSlotController homeSlot;
        [SerializeField] StampType stampType;
    // ==--


    // --== ATTACHED COMPONENTS ==-- //
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D  boxCollider2D; 
    // ==--


    // --== CLASS FIELDS ==-- //
        private StampState currentStampState = StampState.Idle;
    // ==--


    // --== CLASS METHODS ==-- //
        private void HandleLeftClick()
        {
            switch (this.currentStampState)
            {
                case StampState.Idle:
                    this.currentStampState = StampState.Held;
                break;

                case StampState.Held:
                break;

                case StampState.MovingToArea:
                case StampState.Placed:
                break;
            }
        }
    // ==--


    // --== 'IPointerClickHandler' METHODS ==-- //
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                this.HandleLeftClick();
            }
        }
    // ==--


    // --== UNITY METHODS ==-- //
        void Start()
        {
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
            this.boxCollider2D  = this.GetComponent<BoxCollider2D>();
        }

        void Update() 
        { 
            switch (this.currentStampState)
            {
                case StampState.Held:
                    var mousePosition = Mouse.current.position.ReadValue();
                    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    this.transform.position = mousePosition;

                    foreach (
                        BoxCollider2D candidateCollider
                        in FindObjectsByType<BoxCollider2D>(FindObjectsSortMode.None)
                    ) {
                        if (
                            candidateCollider == this.boxCollider2D
                        ) continue;

                        if ( 
                            this.boxCollider2D.IsTouching(candidateCollider)
                            & candidateCollider.gameObject.GetComponent<StampSpaceController>() != null
                        ) {
                            Debug.Log("Stamp is touching: " + candidateCollider.name);
                        }
                    }
                break;

                case StampState.MovingToArea:
                case StampState.Idle:
                case StampState.Placed:
                break;
            }
        } // void Update()

    // ==--
}
