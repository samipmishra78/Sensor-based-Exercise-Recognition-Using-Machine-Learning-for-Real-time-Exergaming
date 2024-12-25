using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class players : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private Vector3 originalCenter;
    private float originalHeight;
    private bool isJumpDown = false;
    private bool gameStarted = false; // Flag to check if the game has started

    public static int CurrentTile = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Save the original collider values
        if (capsuleCollider != null)
        {
            originalCenter = capsuleCollider.center;
            originalHeight = capsuleCollider.height;
        }

        // Set the animator to idle at the start
        animator.SetBool("idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        // Exit the game if Esc is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        if (!gameStarted)
        {
            // Wait for the player to press the W key to start the game
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartRunning();
                gameStarted = true; // Set the game as started
            }
            return; // Prevent other inputs before the game starts
        }

        // Movement and actions during gameplay
        if (Input.GetKey(KeyCode.S)) // Trigger slide
        {
            animator.SetBool("slide", true);
            AdjustColliderForSlide();
        }
        else if (Input.GetKey(KeyCode.Space)) // Trigger jump
        {
            animator.SetBool("jump", true);
            ResetCollider(); // Reset collider after jumping
        }
        else if (Input.GetKey(KeyCode.W)) // Trigger running
        {
            animator.SetBool("run", true);
            animator.SetBool("idle", false); // Ensure idle is off while running
            ResetCollider();
        }
        else
        {
            // Default to idle when no key is pressed
            animator.SetBool("idle", true);
            animator.SetBool("run", false);
            animator.SetBool("slide", false);
            animator.SetBool("jump", false);
            ResetCollider(); // Reset collider to default size and position
        }

        // Check if the "fall" animation is active
        if (animator.GetBool("fall"))
        {
            GameOver();
        }
    }

    void ToggleOff(string Name)
    {
        animator.SetBool(Name, false);
        isJumpDown = false;
    }

    void JumpDown()
    {
        isJumpDown = true;
    }

    private void OnAnimatorMove()
    {
        if (gameStarted) // Only move the player if the game has started
        {
            if (animator.GetBool("jump"))
            {
                if (isJumpDown)
                {
                    rb.MovePosition(rb.position + new Vector3(1, 20, 0) * animator.deltaPosition.magnitude);
                }
                else
                {
                    rb.MovePosition(rb.position + new Vector3(1, 1000f, 2) * animator.deltaPosition.magnitude);
                }
            }
            else
            {
                rb.MovePosition(rb.position + new Vector3(1, 2, 0) * animator.deltaPosition.magnitude);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("obs"))
        {
            animator.SetBool("fall", true);
        }
    }

    // Adjust collider size and position for sliding
    private void AdjustColliderForSlide()
    {
        if (capsuleCollider != null)
        {
            capsuleCollider.height = originalHeight / 2; // Reduce height
            capsuleCollider.center = new Vector3(originalCenter.x, originalCenter.y / 2, originalCenter.z); // Lower center
        }
    }

    // Reset collider to its original size and position
    private void ResetCollider()
    {
        if (capsuleCollider != null)
        {
            capsuleCollider.height = originalHeight;
            capsuleCollider.center = originalCenter;
        }
    }

    // Function to start running
    private void StartRunning()
    {
        animator.SetBool("idle", false); // Turn off idle animation
        animator.SetBool("run", true);  // Start the run animation
    }

    // Function to quit the game
    private void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // Log a message in the Unity editor to simulate quitting
#if UNITY_EDITOR
        Debug.Log("Game is exiting...");
#endif
    }

    // Function to handle game over
    private void GameOver()
    {
        // Load the Game Over scene
        SceneManager.LoadScene("gameover");
    }
}
