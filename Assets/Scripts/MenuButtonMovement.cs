using UnityEngine;

public class MenuButtonMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f; 
    public float maxOffset = 10f; // Maximum offset the buttons can move
    public float boundaryX = 5f; // Boundary in the X-axis
    public float boundaryY = 5f; // Boundary in the Y-axis

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate the new position based on mouse movement
        Vector3 targetPosition = initialPosition + new Vector3(mouseX, mouseY, 0) * maxOffset;

        // Clamp the target position within the boundaries
        targetPosition.x = Mathf.Clamp(targetPosition.x, initialPosition.x - boundaryX, initialPosition.x + boundaryX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, initialPosition.y - boundaryY, initialPosition.y + boundaryY);

        // Move the button towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}

