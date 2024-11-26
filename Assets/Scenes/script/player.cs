
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private Rigidbody rb;
    public static int CurrentTile = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetKey(KeyCode.Space))
          {
              animator.SetBool("run", true);
          }*/
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
        animator.SetBool(Name, false);
    }
   // private bool isJumpDown = false;
    
    private void OnAnimatorMove()
    {

        rb.MovePosition(rb.position + Vector3.forward * animator.deltaPosition.magnitude);
       /* if (animator.GetBool("jump"))
        {

            rigidbody.MovePosition(rigidbody.position + new Vector3(1, 0, 2) * animator.deltaPosition.magnitude);
        }

        else

            rigidbody.MovePosition(rigidbody.position + new Vector3(1, 1.5f, 2) * animator.deltaPosition.magnitude);*/


    }
}
