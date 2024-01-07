using UnityEngine;

public class MenuButtonMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f; // Adjust this to change the speed of button movement
    public float maxOffset = 10f; // Maximum offset the buttons can move
    public float boundaryX = 5f; // Boundary in the X-axis
    public float boundaryY = 5f; // Boundary in the Y-axis

    private Vector3 initialPosition;
    private Vector3 lastMousePosition;

    public bool animDone;

    void Start()
    {
        initialPosition = transform.position;
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {

            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;

            Vector3 targetPosition = initialPosition + new Vector3(mouseDelta.x, mouseDelta.y, 0) * moveSpeed * maxOffset;

            // Clamp the target position within the boundaries
            targetPosition.x = Mathf.Clamp(targetPosition.x, initialPosition.x - boundaryX, initialPosition.x + boundaryX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, initialPosition.y - boundaryY, initialPosition.y + boundaryY);

            // Move the button towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

            lastMousePosition = currentMousePosition;
        
        
    }
}
