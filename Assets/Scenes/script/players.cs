using UnityEngine;

public class players : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public static int CurrentTile = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("slide", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("jump", true);
        }
    }
    void ToggleOff(string Name)
    {
        animator.SetBool(Name,false);
        isJumpDown = false;


    }
    private bool isJumpDown=false;
    void JumpDown()
    {
        isJumpDown = true;
    }
    private void OnAnimatorMove()
    {
         if (animator.GetBool("jump"))
         {
            if (isJumpDown) {
                rb.MovePosition(rb.position + new Vector3(1, 20, 0) * animator.deltaPosition.magnitude); }
            else
                rb.MovePosition(rb.position + new Vector3(1,1000f, 2) * animator.deltaPosition.magnitude);
        }

         else

             rb.MovePosition(rb.position + new Vector3(1,2,0) * animator.deltaPosition.magnitude);
      



    }
}
