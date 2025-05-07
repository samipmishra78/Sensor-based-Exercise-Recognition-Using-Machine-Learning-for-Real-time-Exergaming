using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Players : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private Vector3 originalCenter;
    private float originalHeight;
    private bool isJumpDown = false;
    private bool gameStarted = false;

    public static int totalScore = 0;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip slideSound;
    [SerializeField] private AudioClip runSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private float jumpForce = 1f;
    private AudioSource audioSource;

    private float runSoundCooldown = 0.5f;
    private float lastRunSoundTime = -1f;

    private bool isGrounded;
    private bool canJump = true;
    private float joggingStartTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();

        if (capsuleCollider != null)
        {
            originalCenter = capsuleCollider.center;
            originalHeight = capsuleCollider.height;
        }

        animator.SetBool("idle", true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.W) || listener.receivedMessage == "0")
            {
                StartRunning();
                gameStarted = true;
                joggingStartTime = Time.time;
            }
            return;
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (Input.GetKeyDown(KeyCode.S) || listener.receivedMessage == "2")
        {
            animator.SetBool("slide", true);
            isJumpDown = true;
            PlaySound(slideSound);
            AdjustColliderForSlide();
            IncreaseScore(10);
            GameStats.slideCount++;
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || listener.receivedMessage == "1") && isGrounded && canJump)
        {
            animator.SetBool("jump", true);
            PlaySound(jumpSound);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            Invoke(nameof(ResetJump), 0.5f);
            IncreaseScore(15);
            GameStats.jumpCount++;
        }
        else if (Input.GetKey(KeyCode.W) || listener.receivedMessage == "0")
        {
            animator.SetBool("run", true);
            animator.SetBool("idle", false);
            if (Time.time - lastRunSoundTime > runSoundCooldown)
            {
                PlaySound(runSound);
                lastRunSoundTime = Time.time;
            }
            ResetCollider();
            IncreaseScore(5);
        }
        else
        {
            animator.SetBool("idle", true);
            animator.SetBool("run", false);
            animator.SetBool("slide", false);
            animator.SetBool("jump", false);
            ResetCollider();
        }

        if (animator.GetBool("fall"))
        {
            GameOver();
        }
    }

    void ResetJump()
    {
        canJump = true;
    }

    private void OnAnimatorMove()
    {
        if (gameStarted && !animator.GetBool("jump"))
        {
            rb.MovePosition(rb.position + new Vector3(1, 0, 0) * animator.deltaPosition.magnitude);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("obs"))
        {
            animator.SetBool("fall", true);
            PlaySound(gameOverSound);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void AdjustColliderForSlide()
    {
        if (capsuleCollider != null)
        {
            capsuleCollider.height = originalHeight / 2;
            capsuleCollider.center = new Vector3(originalCenter.x, originalCenter.y / 2, originalCenter.z);
        }
        Invoke(nameof(ResetCollider), 0.5f);
    }

    private void ResetCollider()
    {
        if (capsuleCollider != null)
        {
            capsuleCollider.height = originalHeight;
            capsuleCollider.center = originalCenter;
        }
    }

    private void StartRunning()
    {
        animator.SetBool("idle", false);
        animator.SetBool("run", true);
        PlaySound(runSound);
        rb.AddForce(Vector3.forward * 2f);
    }

    private void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("Game is exiting...");
#endif
    }

    private void GameOver()
    {
        GameStats.finalScore = totalScore;
        GameStats.joggingTime += Time.time - joggingStartTime;
        GameStats.CalculateCaloriesBurned();
        StartCoroutine(HandleGameOver());
    }

    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("gameover");
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void IncreaseScore(int increment)
    {
        totalScore += increment;
        GameStats.finalScore = totalScore;
    }
}
