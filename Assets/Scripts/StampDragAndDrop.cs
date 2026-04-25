using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

enum CurrentStampleState {
    Idle,
    Grabbed,
    Placed,
    Returning
}

public class StampDragAndDrop : MonoBehaviour, IPointerClickHandler
{
    [field: Header("Initial Position")]
    [SerializeField] public float initialStartX = 0.0f;
    [SerializeField] public float initialStartY = 0.0f;

    [field: Header("Timeings")]
    [SerializeField] public float stampTime;
    [SerializeField] public float returnTime;


    Vector3 startPosition;
    Vector3 placedLocation;
    CurrentStampleState currentStampleState = CurrentStampleState.Idle;
    
    float timeTillPositionReset = 0;
    float lerpTimeLeft = 0;




    void Start()
    {
        // Where we'll return to
        startPosition = new(initialStartX, initialStartY, 0);   
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

                if (timeTillPositionReset <= 0)
                {
                    // When the stamp timer is finished, store where we placed it and setup the return journey
                    placedLocation = transform.position;

                    lerpTimeLeft = returnTime;
                    currentStampleState = CurrentStampleState.Returning;
                } else {

                    // Reduce the timer's time left
                    timeTillPositionReset -= Time.deltaTime;
                }
            break;


            case CurrentStampleState.Returning:

                if (lerpTimeLeft <= 0)
                {
                    transform.position = startPosition;  // Just to make sure
                    currentStampleState = CurrentStampleState.Idle;
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
            break;


            case CurrentStampleState.Grabbed:
                timeTillPositionReset = stampTime;
                currentStampleState = CurrentStampleState.Placed;
            break;


            case CurrentStampleState.Placed:
            break;


            case CurrentStampleState.Returning:
            break;
        }}
    }
}
