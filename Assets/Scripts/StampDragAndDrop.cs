using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum CurrentStampleState {
    Idle,
    Grabbed,
    Placed,
    PostPlacedHover,
    Returning,
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class StampDragAndDrop : MonoBehaviour, IPointerClickHandler
{
    // --== SERIALISED FIELDS ==-- //
        [field: Header("Initial Position")]
        [SerializeField] public float initialStartPositionX = 0.0f;
        [SerializeField] public float initialStartPositionY = 0.0f;

        [field: Header("Timeings")]
        [SerializeField] public float stampingTime;
        [SerializeField] public float postStampIdleTime;
        [SerializeField] public float returnTime;

        [field: Header("Sprites")]
        [SerializeField] private Sprite spritePlaced;
        [SerializeField] private Sprite spriteCarried;
        [SerializeField] private Sprite spriteStamped;

        [field: Header("Misc")]

        [SerializeField] public Decision.StampState stampState;
        [SerializeField] private AudioClip stampingSFX;
    // ==--

    // --== STAMP POSITIONS ==-- //
        Vector3 startPosition;
        Vector3 placedPosition;
    // ==--

    // --== ATTACHED COMPONENTS ==-- //
        SpriteRenderer spriteRenderer;
        AudioSource audioSource;
    // ==--

    // --== VARIABLES ==-- //
        public CurrentStampleState currentStampleState = CurrentStampleState.Idle;
        private float stampingTimeLeft = 0;
        private float postStampingIdleTimeLeft = 0;
        private float returnTimeLeft = 0;
    // ==--

    void Start()
    {
        this.startPosition = new Vector3(
            this.initialStartPositionX,
            this.initialStartPositionY,
            0
        );

        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.audioSource    = GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (this.currentStampleState)
        {
            case CurrentStampleState.Idle:
                // Unused
            break;
            // CurrentStampleState.Idle


            case CurrentStampleState.Grabbed:
                // If the stamp is grabbed, move it to the mouse's position
                var mousePosition = Mouse.current.position.ReadValue();
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                transform.position = mousePosition;
            break;
            // CurrentStampleState.Grabbed


            case CurrentStampleState.Placed:
                // If we have waited at least `this.stampingTime`, we should lift the stamp and let it hover a bit
                if (this.stampingTimeLeft <= 0) {
                    this.postStampingIdleTimeLeft = this.postStampIdleTime;

                    this.spriteRenderer.sprite = this.spriteCarried;
                    this.currentStampleState = CurrentStampleState.PostPlacedHover;
                } else {
                    this.stampingTimeLeft -= Time.deltaTime;
                }
            break;
            // CurrentStampleState.Placed


            case CurrentStampleState.PostPlacedHover:
                // If we have waited at least `this.postStampingIdleTime`, we should begin our return journey to the initial start position
                if (this.postStampingIdleTimeLeft <= 0) {
                    this.returnTimeLeft = this.returnTime;

                    this.currentStampleState = CurrentStampleState.Returning;
                } else {
                    this.postStampingIdleTimeLeft -= Time.deltaTime;
                }
            break;
            // CurrentStampleState.PostPlacedHover


            case CurrentStampleState.Returning:
                // If we have waited at least `this.returnTime`, we should place the stamp back down in it's resting spot
                // If not however, we should put the stamp at the correct spot between the two positions
                if (this.returnTimeLeft <= 0) {
                    this.transform.position = this.startPosition;    // Just to make sure

                    this.spriteRenderer.sprite = this.spritePlaced;
                    this.currentStampleState = CurrentStampleState.Idle;
                } else {
                    this.returnTimeLeft -= Time.deltaTime;

                    float currentLerpPosition = this.returnTimeLeft / this.returnTime;    // Goes 100% -> 0%
                    
                    this.transform.position = Vector3.Lerp(
                        this.startPosition,     // Position at   0%
                        this.placedPosition,    // Position at 100%
                        currentLerpPosition
                    );
                }
            break;
            // CurrentStampleState.Returning
        }
        // switch (this.currentStampleState)
    }
    // void Update()



    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left) {
            switch (this.currentStampleState)
        {
            case CurrentStampleState.Idle:
                this.currentStampleState = CurrentStampleState.Grabbed;
                this.spriteRenderer.sprite = this.spriteCarried;
            break;
            // CurrentStampleState.Idle

            case CurrentStampleState.Grabbed:
                this.stampingTimeLeft = this.stampingTime;
                this.audioSource.PlayOneShot(
                    this.stampingSFX
                );

                this.placedPosition = this.transform.position;
                this.currentStampleState = CurrentStampleState.Placed;
                this.spriteRenderer.sprite = this.spriteStamped;
            break;
            // CurrentStampleState.Grabbed
            
            case CurrentStampleState.Placed:
            case CurrentStampleState.PostPlacedHover:
            case CurrentStampleState.Returning:
                // Unused
            break;
        }}
        // if (pointerEventData.Button == PointerEventData.InputButton.Left) 
        //     switch (this.currentStampleState)
    }
    // public void OnPointerClick(PointerEventData)
}
