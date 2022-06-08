using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private float velocity;
    private float accelRatePerSec;
    private float decelRatePerSec;

    [HideInInspector]
    public bool isGrounded = false;
    private bool jumpButtPress = false;

    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float maxSpeed = 5;

    public float timeZeroToMax = 0.2f;
    public float timeMaxToZero = 0.2f;

    public float maxJumpVelocity = 9;
    public float minJumpVelocity = 4;

    public float fallMultipler = 3;

    public float wallRaycasOffset = 0.25f;
    public float groundRaycasOffset = 0;

    public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {   // Get Rigidbody Component
        myRigidbody = GetComponent<Rigidbody2D>();
        // Set Acceleration and Deacceleration
        accelRatePerSec = (maxSpeed / timeZeroToMax) * Time.deltaTime;
        decelRatePerSec = (maxSpeed / timeMaxToZero) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Horizontal input
        float hi = Input.GetAxisRaw("Horizontal");

        // Set Movement Rotation
        if (hi > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (hi < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        // Set Velocity
        if (hi != 0)
        {
            velocity += Input.GetAxisRaw("Horizontal") * accelRatePerSec;
        }
        else if (velocity > 1)
        {
            velocity -= decelRatePerSec;
        }
        else if (velocity < 0)
        {
            velocity += decelRatePerSec;
        }
        else
        {
            velocity = 0;
        }

        // Clamp Velocity
        velocity = Mathf.Clamp(velocity, -maxSpeed, maxSpeed);

        // Set Run
        if (Input.GetButtonDown("Fire1"))
        {
            maxSpeed = runSpeed;
        }
 
        // Run Reset
        if (hi == 0)
        {
            maxSpeed = walkSpeed;
        }

        // Simple Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, maxJumpVelocity);
            jumpButtPress = true;
        }

        if (Input.GetButtonUp("Jump") && jumpButtPress)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, minJumpVelocity);
            jumpButtPress = false;
            myRigidbody.gravityScale = fallMultipler;
        }

        

    }


    void FixedUpdate()
    {
         

        // Reycast
        RaycastHit2D wallHit = Physics2D.BoxCast(transform.position, transform.localScale/2, 0f, transform.right, wallRaycasOffset, layerMask);
        if(wallHit.collider != null)
        {
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
        }

        RaycastHit2D groundHit = Physics2D.BoxCast(transform.position, transform.localScale/2, 0f, Vector3.down, groundRaycasOffset, layerMask);
        if (groundHit.collider != null)
        {
            isGrounded = true;
            myRigidbody.gravityScale = 1;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0,-groundRaycasOffset,0), transform.localScale/2);
        Gizmos.DrawWireCube(transform.position + new Vector3(wallRaycasOffset, 0, 0), transform.localScale/2);
    }

    void LateUpdate()
    {
        myRigidbody.velocity = new Vector2(velocity, myRigidbody.velocity.y);
    }


}
