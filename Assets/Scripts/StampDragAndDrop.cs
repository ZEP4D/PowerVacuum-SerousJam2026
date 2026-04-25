using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

enum CurrentStampleState {
    Idle,
    Grabbed,
    Placed
}

public class StampDragAndDrop : MonoBehaviour, IPointerClickHandler
{
    [field: Header("Initial Position")]
    [SerializeField] public float initialStartX = 0.0f;
    [SerializeField] public float initialStartY = 0.0f;


    [field: Header("Stamp Time")]
    [SerializeField] public float stampTime = 1.0f;




    Vector3 initialStartPosition;
    CurrentStampleState currentStampleState = CurrentStampleState.Idle;
    Vector2 lastPosition = new(0,0);
    float timeTillPositionReset = 0;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialStartPosition = new(initialStartX, initialStartY, 0);   
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStampleState) {
            case CurrentStampleState.Idle:
            break;

            case CurrentStampleState.Grabbed:
                var mousePosition = Mouse.current.position.ReadValue();
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                transform.position = mousePosition;
            break;

            case CurrentStampleState.Placed:
                if (timeTillPositionReset <= 0) {
                    currentStampleState = CurrentStampleState.Idle;
                    transform.position = initialStartPosition;
                } else {
                    timeTillPositionReset -= Time.deltaTime;
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
                lastPosition = transform.position;
                timeTillPositionReset = stampTime;
                currentStampleState = CurrentStampleState.Placed;
            break;

            case CurrentStampleState.Placed:
                // Nothing rn
            break;
        }}
    }
}
