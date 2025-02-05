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
    private bool isJumpDown = false;
    private bool gameStarted = false;

    public static int totalScore = 0;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip slideSound;
    [SerializeField] private AudioClip runSound;
    [SerializeField] private AudioClip gameOverSound;
    private AudioSource audioSource;

    private float runSoundCooldown = 0.5f;
    private float lastRunSoundTime = -1f;

    void Start()
    {
        animator = GetComponent<Animator>();
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartRunning();
                gameStarted = true;
            }
            return;
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("slide", true);
            PlaySound(slideSound);
            AdjustColliderForSlide();
            IncreaseScore(10);
            GameStats.slideCount++;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("jump", true);
            PlaySound(jumpSound);
            ResetCollider();
            IncreaseScore(15);
            GameStats.jumpCount++;
        }
        else if (Input.GetKey(KeyCode.W))
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
            GameStats.joggingTime += Time.deltaTime;
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

    private void OnAnimatorMove()
    {
        if (gameStarted)
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

    private void AdjustColliderForSlide()
    {
        if (capsuleCollider != null)
        {
            capsuleCollider.height = originalHeight / 2;
            capsuleCollider.center = new Vector3(originalCenter.x, originalCenter.y / 2, originalCenter.z);
        }
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
    }
}
