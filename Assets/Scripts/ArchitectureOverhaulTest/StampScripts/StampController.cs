using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using System;
using System.Text;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class StampController : MonoBehaviour, IPointerClickHandler
{
    // --== SERIALIZED FIELDS ==-- //
        [SerializeField] StampSlotController homeSlot;
        [SerializeField] StampType stampType;
    // ==--


    // --== ATTACHED COMPONENTS ==-- //
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D  boxCollider2D;
        private Rigidbody2D    rigidbody2D;
    // ==--


    // --== CLASS FIELDS ==-- //
        private StampState currentStampState = StampState.Idle;
        private Vector2 targetPosition;
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
            this.rigidbody2D    = this.GetComponent<Rigidbody2D>();
        }

        void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("Touched: " + other.gameObject.name);
        }

        void Update() 
        { 
            switch (this.currentStampState)
            {
                case StampState.Held:
                    var mousePosition = Mouse.current.position.ReadValue();
                    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    this.rigidbody2D.position = mousePosition;
                break;

                case StampState.MovingToArea:
                case StampState.Idle:
                case StampState.Placed:
                break;
            }
        } // void Update()

    // ==--
}
