using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBody;
    BoxCollider2D myFoot;
    float defaultGravity;
    bool isAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<CapsuleCollider2D>();
        myFoot = GetComponent<BoxCollider2D>();
        defaultGravity = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive)
            return;
        Die();
        Run();
        Jump();
        Climb();
        Flip();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        myAnimator.SetBool("run", controlThrow != 0);

        Vector2 runVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = runVelocity;
    }

    private void Flip()
    {
        if (myRigidbody.velocity.x == 0)
            return;

        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    private void Jump()
    {
        if (!myFoot.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocity;
        }
    }

    private void Climb()
    {
        if (!myBody.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.SetBool("climb", false);
            myRigidbody.gravityScale = defaultGravity;
            return;
        }

        myRigidbody.gravityScale = 0f;

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        myAnimator.SetBool("climb", controlThrow != 0);

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
    }

    private void Die()
    {
        if (myBody.IsTouchingLayers(LayerMask.GetMask("Guard", "Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("die");

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
