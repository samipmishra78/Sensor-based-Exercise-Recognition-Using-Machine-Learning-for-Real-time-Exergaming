using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Required for UI components
using System.Collections; // Required for coroutines

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

    public static int totalScore = 0; // Total score of the player

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip slideSound;
    [SerializeField] private AudioClip runSound;
    [SerializeField] private AudioClip gameOverSound;
    private AudioSource audioSource;

    private float runSoundCooldown = 0.5f; // Cooldown for running sound
    private float lastRunSoundTime = -1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();

        // Check and log AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }

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
            PlaySound(slideSound);
            AdjustColliderForSlide();
            IncreaseScore(10); // Increase score for sliding
        }
        else if (Input.GetKey(KeyCode.Space)) // Trigger jump
        {
            animator.SetBool("jump", true);
            PlaySound(jumpSound);
            ResetCollider(); // Reset collider after jumping
            IncreaseScore(15); // Increase score for jumping
        }
        else if (Input.GetKey(KeyCode.W)) // Trigger running
        {
            animator.SetBool("run", true);
            animator.SetBool("idle", false); // Ensure idle is off while running
            if (Time.time - lastRunSoundTime > runSoundCooldown)
            {
                PlaySound(runSound);
                lastRunSoundTime = Time.time;
            }
            ResetCollider();
            IncreaseScore(5); // Increase score for running
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
            PlaySound(gameOverSound);
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
        PlaySound(runSound);
    }

    // Function to quit the game
    private void QuitGame()
    {
        Application.Quit();

        // Log a message in the Unity editor to simulate quitting
#if UNITY_EDITOR
        Debug.Log("Game is exiting...");
#endif
    }

    // Function to handle game over
    private void GameOver()
    {
        StartCoroutine(HandleGameOver());
    }

    // Coroutine to handle game-over delay
    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(1f); // Wait for 5 seconds
        SceneManager.LoadScene("gameover"); // Load the game-over scene
    }

    // Function to play sound
    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing!");
            return;
        }

        if (clip == null)
        {
            Debug.LogWarning("AudioClip is not assigned for this action!");
            return;
        }

        Debug.Log($"Playing sound: {clip.name}");
        audioSource.PlayOneShot(clip);
    }

    // Function to increase score
    private void IncreaseScore(int increment)
    {
        totalScore += increment;
        Debug.Log($"Score: {totalScore}");
    }
}