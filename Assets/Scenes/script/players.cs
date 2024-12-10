using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class players : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private Vector3 originalCenter;
    private float originalHeight;
    private bool gameStarted = false; // Flag to check if the game has started
    private bool isGameOver = false; // Flag to prevent multiple triggers of game over

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
            // Wait for the player to press the space bar to start the game
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartRunning();
                gameStarted = true; // Set the game as started
            }
            return; // Prevent other inputs before the game starts
        }

        if (!isGameOver)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        // Jump logic (trigger on Space key press)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("jump", true); // Trigger jump animation
            StartCoroutine(ResetAnimation("jump")); // Reset jump animation after a short delay
        }

        // Slide logic (trigger on S key press)
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("slide", true); // Trigger slide animation
            AdjustColliderForSlide(); // Adjust the collider for sliding
        }
        else
        {
            animator.SetBool("slide", false); // Reset slide animation
            ResetCollider(); // Reset collider to original values
        }

        // Running logic (trigger on W key press)
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("run", true); // Trigger running animation
        }
        else
        {
            animator.SetBool("run", false); // Reset running animation
        }

        // Default state: idle
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("idle", true);
        }
        else
        {
            animator.SetBool("idle", false);
        }
    }

    private IEnumerator ResetAnimation(string animationName)
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay based on your animation duration
        animator.SetBool(animationName, false); // Reset the animation
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("obs") && !isGameOver)
        {
            isGameOver = true; // Prevent multiple triggers of game over
            TriggerGameOverSequence();
        }
    }

    private void TriggerGameOverSequence()
    {
        animator.SetBool("fall", true); // Trigger fall (dead) animation
        StartCoroutine(GameOverCoroutine()); // Start the coroutine to delay game over screen
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        SceneManager.LoadScene("gameover"); // Load the game over screen
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
}
