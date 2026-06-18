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
        [SerializeField] public StampType stampType;

        [field: Header("Timings")]
        [SerializeField] float stampLerpTravelTime = 0.25f;
        [SerializeField] float stampPlaceTime = 0.5f;
        [SerializeField] float stampMarkLifetime = 5f;
        [SerializeField] float stampMarkFadeTime = 1f;

        [field: Header("Sprites")]
        [SerializeField] Sprite stampMarkSprite;
        [SerializeField] Sprite stampPloppedSprite;
        [SerializeField] Sprite stampHeldSprite;
        [SerializeField] Sprite stampStampedSprite;

        [field: Header("Misc")]
        [SerializeField] private int orderInLayerGrabbed = 11;
        [SerializeField] private int orderInLayerIdle = 10;

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

        private GameObject interactableDue;
    // ==--


    // --== CLASS METHODS ==-- //
        /// Checks wether stamp is hovering over an accessable interactable (overlapping all 4 corner colliders),
        /// and if so, returns the GameObject of that interactable.
        /// NOTE: GameObjects are REQUIRED to implement `IStampIntetractable` in order to be returnable by this
        /// method
        #nullable enable
        public GameObject? GetInteractable() {
            // Get a list of all overlapping colliders
            List<Collider2D> colliders = new();
            if (this.GetComponent<Rigidbody2D>().Overlap(colliders) < 4) {
                // We aren't hovering over enough colliders to be over all 4 of an interactable
                return null;
            }

            // Iterate over all overlapping colliders and sort them by owning gameobject
            Dictionary<GameObject, List<Collider2D>> gameObjectColliders = new();
            foreach (Collider2D collider in colliders)
            {
                if (!gameObjectColliders.ContainsKey(collider.gameObject)) gameObjectColliders.Add(collider.gameObject, new());
                gameObjectColliders[collider.gameObject].Add(collider);
            }

            // If hovering over all 4 colliders of a gameobject, it will be the interactable we are looking for, so return it
            foreach (KeyValuePair<GameObject, List<Collider2D>> pair in gameObjectColliders)
            {
                if (
                    pair.Value.Count == 4
                    & pair.Key.GetComponent<IStampInteractable>() != null
                ) return pair.Key;
            }

            // Or if we are not, then there is no interactable
            return null;
        }

        private void SetStampState(StampState stampStateIn)
        {
            this.currentStampState = stampStateIn;
            switch(stampStateIn)
            {
                case StampState.MovingToArea:
                    this.lerpTimeLeft = this.stampLerpTravelTime;
                break;
                
                case StampState.Placed:
                    this.spriteRenderer.sprite = this.stampStampedSprite;
                    this.placedTimeLeft = this.stampPlaceTime;
                break;
                
                case StampState.ReturnToMouse:
                    this.spriteRenderer.sprite = this.stampHeldSprite;
                    this.lerpTimeLeft = this.stampLerpTravelTime;
                break;

                case StampState.Idle:
                    this.spriteRenderer.sprite = this.stampPloppedSprite;
                    this.spriteRenderer.sortingOrder = this.orderInLayerIdle;
                break;

                case StampState.Held:
                    this.spriteRenderer.sprite = this.stampHeldSprite;
                    this.spriteRenderer.sortingOrder = this.orderInLayerGrabbed;
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
                    // See if we are hovering over an interactable, if we are not, spawn a stamp mark
                    GameObject? interactable = this.GetInteractable();

                    if (interactable is not null) {
                        this.SetStampState(StampState.MovingToArea);

                        this.lerpPositionBegin = this.GetComponent<Rigidbody2D>().position;
                        this.lerpPositionEnd   = interactable.transform.position;

                        this.interactableDue = interactable;
                        
                        return;

                    } else {
                        this.SetStampState(StampState.Placed);
                        GameObject spawnedStampMark = new("perishable_stamp_mark");

                        // Add before script as unity will add it for us, blocking us from accessing it
                        SpriteRenderer spawnedStampMarkSpriteRenderer = spawnedStampMark.AddComponent<SpriteRenderer>();
                        spawnedStampMarkSpriteRenderer.sprite = this.stampMarkSprite;

                        StampMarkController spawnedStampMarkController = spawnedStampMark.AddComponent<StampMarkController>();
                        spawnedStampMarkController.fadeTime = this.stampMarkFadeTime;
                        spawnedStampMarkController.lifetimeRemaining = this.stampMarkLifetime;

                        spawnedStampMark.transform.position = this.gameObject.transform.position;
                    }
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
                        
                        if (this.interactableDue.gameObject.GetComponent<StampSlotController>() != null) this.SetStampState(StampState.Idle);
                        else this.SetStampState(StampState.Placed);

                        // This will throw an exeption if `this.interactableDue` is not an `IStampInteractable`, which SHOULD NOT ever be the case
                        this.interactableDue?
                            .GetComponent<IStampInteractable>()
                            .InteractPrimary(this);
                        
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

                if (count == 4) other
                    .gameObject
                    .GetComponent<StampSpaceController>()?
                    .SetHighlight(true);
            }
        }
        void OnTriggerExit2D(Collider2D other) {
            // If we're exiting any collider, then we're not enough to keep that collider's GameObject highlighted
            other
                .gameObject
                .GetComponent<StampSpaceController>()?
                .SetHighlight(false);
        }
    // ==--
}
