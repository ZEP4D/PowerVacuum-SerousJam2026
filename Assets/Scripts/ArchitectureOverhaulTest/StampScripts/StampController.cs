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
                    // If the count is less than 4, it means we are not overlapping enough colliders to be overlapping all 4 corners of an interactable
                    // - In this case, we tell the stamp to place itself, then break early
                    List<Collider2D> colliders = new();
                    if (this.GetComponent<Rigidbody2D>().Overlap(colliders) < 4) {
                        this.SetStampState(StampState.Placed);
                        break; // Exit early
                    }

                    // If we are overlapping at least 4 colliders, we might be doing so for all 4 corner colliders of an interactable, which brings us here
                    // - We need to both verify if we are overlapping over all 4 corner colliders of an interactable, and also get the GameObject associated
                    //   With it in order to tell the stamp who to interact with. To do that we group colliders by GameObject, and if a group has 4, that
                    //   means that GameObject is an interactable
                    Dictionary<GameObject, List<Collider2D>> gameObjectColliders = new();
                    foreach (Collider2D collider in colliders)
                    {
                        if (!gameObjectColliders.ContainsKey(collider.gameObject)) gameObjectColliders.Add(collider.gameObject, new());
                        gameObjectColliders[collider.gameObject].Add(collider);
                    }

                    foreach (KeyValuePair<GameObject, List<Collider2D>> pair in gameObjectColliders)
                    {
                        if (pair.Value.Count == 4)
                        {
                            // Move stamp to interactable's position, and tell it to stamp down
                            this.SetStampState(StampState.MovingToArea);
                            this.lerpPositionBegin = this.GetComponent<Rigidbody2D>().position;
                            this.lerpPositionEnd   = pair.Key.gameObject.transform.position;

                            // We got one, no need to go look for any more
                            // And if for some reason there is another candidate, then first detected, first interacted
                            return;
                        }
                    }

                    // If there were no appropriate candidates, just stamp down
                    this.SetStampState(StampState.Placed);
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
                        
                        // Interact with the interacatable
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
            // In order to set the highlight of an interactable, we need to see if we are overlapping all of their corner colliders

            List<Collider2D> colliders = new();
            if (this.GetComponent<Rigidbody2D>().Overlap(colliders) < 4) return; // We need to overlap all 4 corners

            int count = 0;
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == other.gameObject) count++;
                if (count == 4) other.gameObject.GetComponent<StampSpaceController>()?.SetHighlight(true);
            }
        }
        void OnTriggerExit2D(Collider2D other) {
            // If we're exiting any collider, then we're not enough to keep that collider's GameObject highlighted
            other.gameObject.GetComponent<StampSpaceController>()?.SetHighlight(false);
        }
    // ==--
}
