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
        [field: Header("Setup")]
        [SerializeField] StampSlotController homeSlot;
        [SerializeField] StampType stampType;

        [field: Header("Timing Values")]
        [SerializeField] float stampLerpTravelTime = 0.25f;
        [SerializeField] float stampPlaceTime = 0.5f;
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
        private float placedTimeLeft;
    // ==--


    // --== CLASS METHODS ==-- //
        private void SetStampState(StampState stampStateIn)
        {
            this.currentStampState = stampStateIn;
            switch(stampStateIn)
            {
                case StampState.MovingToArea:
                    this.lerpTimeLeft = this.stampLerpTravelTime;
                break;
                
                case StampState.Placed:
                    this.placedTimeLeft = this.stampPlaceTime;
                break;
                
                case StampState.ReturnToMouse:
                    this.lerpTimeLeft = this.stampLerpTravelTime;
                break;

                case StampState.Idle:
                case StampState.Held:
                break;
            }
        }

        private void HandleInteractionPrimary()
        {
            switch (this.currentStampState)
            {
                case StampState.Idle:
                    this.SetStampState(StampState.Held);
                break;

                case StampState.Held:
                    // Get a list of all colliders we are hovering over;
                    // - If it's empty, that means we aren't interacting with anything, and can move to 'StampState.Placed'
                    // - If not, we take the first element, then set the state to 'StampState.MovingToArea', which will set
                    //   up the interaction after it is done
                    List<Collider2D> colliders = new();
                    if (this.GetComponent<Rigidbody2D>().Overlap(colliders) == 0) {
                        this.SetStampState(StampState.Placed);
                        break; // Exit early
                    } else {
                        this.SetStampState(StampState.MovingToArea);
                    }

                    // We tell the stamp where we currently are, and where we need to be
                    this.lerpPositionBegin = this.GetComponent<Rigidbody2D>().position;
                    this.lerpPositionEnd   = colliders[0].gameObject.transform.position;
                break;

                case StampState.MovingToArea:
                case StampState.Placed:
                case StampState.ReturnToMouse:
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
                    if (this.lerpTimeLeft <= 0) {
                        // Place the stamp down and interact with the given interactable
                        this.SetStampState(StampState.Placed);

                        List<Collider2D> colliders = new();
                        this.GetComponent<Rigidbody2D>().Overlap(colliders);
                        GameObject? candidateCollider = colliders[0].gameObject;
                        
                        if (candidateCollider.GetComponent<StampSlotController>() != null) {
                            this.SetStampState(StampState.Idle);
                        }
                        if (candidateCollider.GetComponent<StampSpaceController>() != null) {
                            this.SetStampState(StampState.Placed);
                        }
                        
                    } else {
                        this.lerpTimeLeft -= Time.deltaTime;
                        this.GetComponent<Rigidbody2D>().position  = Vector2.Lerp(
                            this.lerpPositionEnd,
                            this.lerpPositionBegin,
                            ( this.lerpTimeLeft / this.stampLerpTravelTime)
                        );
                    }
                break;

                case StampState.Placed:
                    if (this.placedTimeLeft <= 0) {
                        this.SetStampState(StampState.ReturnToMouse);
                        this.lerpPositionBegin = this.GetComponent<Rigidbody2D>().position;
                    } else {
                        this.placedTimeLeft -= Time.deltaTime;
                    }
                break;
                
                case StampState.ReturnToMouse:
                    if (this.lerpTimeLeft <= 0) {
                        this.SetStampState(StampState.Held);
                    } else {
                        this.lerpTimeLeft -= Time.deltaTime;
                        this.GetComponent<Rigidbody2D>().position = Vector2.Lerp(
                            Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue() ),
                            this.lerpPositionBegin,
                            ( this.lerpTimeLeft / this.stampLerpTravelTime )
                        );
                    }
                break;

                case StampState.Idle:
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
