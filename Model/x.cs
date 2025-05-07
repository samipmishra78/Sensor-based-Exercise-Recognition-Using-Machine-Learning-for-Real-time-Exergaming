//using UnityEngine;
//using System.Collections;

//public class tforwardJump : MonoBehaviour
//{
//    private Vector3 Playermovementinput;
//    private bool isGrounded = true;
//    private bool hasJumped = false; //  New flag to ensure jump happens only once
//     private float animationSpeed = 0f;

//    [SerializeField] private Rigidbody rb;
//    [SerializeField] private float Speed = 3f;
//    [SerializeField] private float Jumpforce = 3f;
//    [SerializeField] private float forwardForce = 10f;
//    [SerializeField] private Animator animator; 
    

//    private void Update() // naya void update
//    {
//        //bool receivedRunInput = udp_listener.receivedMessage == "0" || Input.GetKey(KeyCode.W);
//        //bool receivedJumpInput = udp_listener.receivedMessage == "1" || Input.GetKey(KeyCode.Space);
//        // bool receivedIdleInput = udp_listener.receivedMessage == "2";
//        // float moveSpeed = 0f; // deafult speed 

//        float targetSpeed = receivedRunInput ? 1f : 0f;
//        //  float targetSpeed = udp_listener.receivedMessage == "" ? 0f : float.Parse(udp_listener.receivedMessage);
//        animationSpeed = Mathf.Lerp(animationSpeed, targetSpeed, Time.deltaTime * 5f); // Smooth transition

//        // Apply the smoothed Speed value
//        animator.SetFloat("Speed", animationSpeed);



//        if (receivedRunInput && !hasJumped) 
//        {
//            Playermovementinput = Vector3.forward;
//            MovePlayer();
//            // moveSpeed= 1f;
//            // animator.SetBool("isRunning", true);  // ✅ Switch to Running animation
//        }
//        // else if (!receivedRunInput) 
//        // {
//        //     // animator.SetBool("isRunning", false); // ✅ Switch back to Idle animation
//        //     moveSpeed =0f;
//        // }

//        if (receivedJumpInput && isGrounded && !hasJumped) 
//        {
//            hasJumped = true;
//            // animator.SetTrigger("Jump"); // new
//            Jump();
//        }

//        // animator.SetFloat("Speed", moveSpeed); // new
//    }

//     private void MovePlayer()
//    {
//        Vector3 MoveVector = transform.TransformDirection(Playermovementinput) * Speed;
//        rb.linearVelocity = new Vector3(MoveVector.x, rb.linearVelocity.y, MoveVector.z);
//    }

//    private void Jump()
//    {   
//        isGrounded = false;
//        animator.SetTrigger("Jump");
//        StartCoroutine(JumpCoroutine());
//    }

//    private IEnumerator JumpCoroutine() //yo part herna baki
//    {   
         
//        float duration = 0.9f;
//        float elapsedTime = 0;
//        Vector3 startPosition = transform.position;
//        Vector3 endPosition = startPosition + (transform.forward * forwardForce);

//        while (elapsedTime < duration)
//        {
//            float height = Mathf.Sin((elapsedTime / duration) * Mathf.PI) * Jumpforce;
//            Vector3 targetPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
//            targetPosition.y += height;
//            rb.MovePosition(targetPosition);

//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }

//        isGrounded = true;
//        hasJumped =false; // reset jump flag
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = true;
//            hasJumped = false; // reset jump flag
//        }
//    }
//}