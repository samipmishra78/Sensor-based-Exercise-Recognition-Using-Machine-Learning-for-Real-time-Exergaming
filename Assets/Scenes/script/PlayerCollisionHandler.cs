using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private Rigidbody rb;
    private bool isBlocked = false;

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component missing from the player. Please add a Rigidbody.");
        }
    }

    void Update()
    {
        // If the player is blocked, prevent movement
        if (isBlocked)
        {
            // Optionally, you can implement additional logic here, such as playing a sound or displaying a message
            // For example:
            // Debug.Log("Player is blocked by a solid object!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "SolidObject"
        if (collision.gameObject.CompareTag("obs"))
        {
            // Stop the player's current velocity to prevent passing through
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Optionally, freeze the player's position and rotation to make them "stuck"
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

            // Set the blocked flag to true
            isBlocked = true;

            // Optional: Notify other systems or trigger events
            // Example: EventManager.TriggerEvent("PlayerBlocked");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the object exiting collision has the tag "SolidObject"
        if (collision.gameObject.CompareTag("obs"))
        {
            // Remove the constraints to allow movement again
            rb.constraints = RigidbodyConstraints.None;

            // Reset the blocked flag
            isBlocked = false;

            // Optional: Notify other systems or trigger events
            // Example: EventManager.TriggerEvent("PlayerUnblocked");
        }
    }
}
