using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [TextArea]
    public string Notes = "The script sends the variables HSpeed and VSpeed to the Animator component";
    private Animator myAnimator;
    private Rigidbody2D myRigidbody;
    private PlatformerMovement pMovement;
    // Start is called before the first frame update
    void Start()
    {
        // Get Animator Component
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        pMovement = GetComponent<PlatformerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set Animator Parameters
        myAnimator.SetFloat("hSpeed", Mathf.Abs(myRigidbody.velocity.x));
        myAnimator.SetFloat("vSpeed", myRigidbody.velocity.y);
        myAnimator.SetBool("isGrounded", pMovement.isGrounded);
    }
}
