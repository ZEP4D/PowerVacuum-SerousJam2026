using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using System;
using System.Text;
using System.Collections.Generic;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class StampController : MonoBehaviour, IPointerClickHandler
{
    // --== SERIALIZED FIELDS ==-- //
        [field: Header("GameObjects")]
        [SerializeField] StampSlotController homeSlot;
        [SerializeField] StampType stampType;

        [field: Header("Timing Values")]
        [SerializeField] float lerpTravelTime = 0.25f;
        [SerializeField] float stampDownTime = 0.5f;
    // ==--


    // --== ATTACHED COMPONENTS ==-- //
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D  boxCollider2D;
    // ==--


    // --== CLASS FIELDS ==-- //
        private StampState currentStampState = StampState.Idle;
        private Vector2 targetPosition;

        private Vector2 lerpPositionBegin;
        private Vector2 lerpPositionEnd;
        private float lerpTimeLeft;
    // ==--


    // --== CLASS METHODS ==-- //
        private void HandleInteractionPrimary()
        {
            switch (this.currentStampState)
            {
                case StampState.Idle:
                    this.currentStampState = StampState.Held;
                break;

                case StampState.Held:
                    // Get a list of all colliders we are hovering over;
                    // - If it's empty, that means we aren't interacting with anything, and can move to 'StampState.Placed'
                    // - If not, we take the first element, then set the state to 'StampState.MovingToArea', which will set
                    //   up the interaction after it is done
                    List<Collider2D> colliders = new();
                    if (this.GetComponent<Rigidbody2D>().Overlap(colliders) == 0) {
                        this.currentStampState = StampState.Placed;
                        break; // Exit early
                    } else  {
                        this.currentStampState = StampState.MovingToArea;
                    }

                    // We reset the timer so that we can refer to it to know where we are in our lerping journey. Along with
                    // that, we take note of our current and target positions to know what line to lerp across
                    this.lerpTimeLeft = this.lerpTravelTime;
                    this.lerpPositionBegin = this.GetComponent<Rigidbody2D>().position;
                    this.lerpPositionEnd   = colliders[0].gameObject.transform.position;
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
                this.HandleInteractionPrimary();
            }
        }
    // ==--


    // --== UNITY METHODS ==-- //
        void Start()
        {
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
            this.boxCollider2D  = this.GetComponent<BoxCollider2D>();
            //this.rigidbody2D    = this.GetComponent<Rigidbody2D>();
        }


        void Update() 
        { 
            switch (this.currentStampState)
            {
                case StampState.Held:
                    var mousePosition = Mouse.current.position.ReadValue();
                    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    this.GetComponent<Rigidbody2D>().position = mousePosition;
                break;

                case StampState.MovingToArea:
                case StampState.Idle:
                case StampState.Placed:
                break;
            }
        } // void Update()


        void OnTriggerEnter2D(Collider2D other) {
            other.gameObject.GetComponent<StampSpaceController>()?.SetHighlight(true);
        }
        void OnTriggerExit2D(Collider2D other) {
            other.gameObject.GetComponent<StampSpaceController>()?.SetHighlight(false);
        }
    // ==--
}
