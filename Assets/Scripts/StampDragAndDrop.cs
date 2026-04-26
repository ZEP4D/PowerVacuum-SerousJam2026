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
    Returning
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class StampDragAndDrop : MonoBehaviour, IPointerClickHandler
{
    [field: Header("Initial Position")]
    [SerializeField] public float initialStartX = 0.0f;
    [SerializeField] public float initialStartY = 0.0f;

    [field: Header("Timeings")]
    [SerializeField] public float stampTime;
    [SerializeField] public float stampIdleTime;
    [SerializeField] public float returnTime;

    [field: Header("Sprites")]
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;

    [field: Header("Misc")]

    [SerializeField] public Decision.StampState stampState;
    [SerializeField] private AudioClip stampSFX;




    Vector3 startPosition;
    Vector3 placedLocation;
    public CurrentStampleState currentStampleState = CurrentStampleState.Idle;


    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    
    float timeTillStampLiftReset = 0;
    float timeTillStampReturns = 0;
    float lerpTimeLeft = 0;





    void Start()
    {
        // Where we'll return to
        startPosition = new(initialStartX, initialStartY, 0);   
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        spriteRenderer.sprite = downSprite;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStampleState) {
            case CurrentStampleState.Idle:
            break;


            case CurrentStampleState.Grabbed:

                // Read the current mouse position in worldspace and move the cursor there
                var mousePosition = Mouse.current.position.ReadValue();
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                transform.position = mousePosition;
            break;


            case CurrentStampleState.Placed:

                if (timeTillStampLiftReset <= 0)
                {
                    timeTillStampReturns = stampIdleTime;

                    // Lift the stamp
                    spriteRenderer.sprite = upSprite;
                    currentStampleState = CurrentStampleState.PostPlacedHover;
                } else {

                    // Reduce the timer's time left
                    timeTillStampLiftReset -= Time.deltaTime;
                }
            break;


            case CurrentStampleState.PostPlacedHover:

                if (timeTillStampReturns <= 0)
                {
                    // When the stamp timer is finished, store where we placed it and setup the return journey
                    placedLocation = transform.position;
                    lerpTimeLeft = returnTime;

                    currentStampleState = CurrentStampleState.Returning;
                } else {
                    timeTillStampReturns -= Time.deltaTime;
                }
            break;


            case CurrentStampleState.Returning:

                if (lerpTimeLeft <= 0)
                {
                    transform.position = startPosition;  // Just to make sure
                    currentStampleState = CurrentStampleState.Idle;
                    spriteRenderer.sprite = downSprite;
                } else {

                    lerpTimeLeft -= Time.deltaTime;
                    float lerpPosition = lerpTimeLeft / returnTime;
                    // 0.0 => Placed Position
                    // 1.0 => Return Position

                    transform.position = Vector3.Lerp(
                        startPosition,
                        placedLocation,
                        lerpPosition
                    );

                }
            break;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) {switch (currentStampleState) {
            case CurrentStampleState.Idle:
                currentStampleState = CurrentStampleState.Grabbed;
                spriteRenderer.sprite = upSprite;
            break;


            case CurrentStampleState.Grabbed:
                timeTillStampLiftReset = stampTime;

                // Place it down
                audioSource.PlayOneShot(stampSFX, 1);
                currentStampleState = CurrentStampleState.Placed;
                spriteRenderer.sprite = downSprite;
            break;


            case CurrentStampleState.Placed:
            break;


            case CurrentStampleState.PostPlacedHover:
            break;


            case CurrentStampleState.Returning:
            break;
        }}
    }
}
