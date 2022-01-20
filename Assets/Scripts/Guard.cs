using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed *= -1;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!this.enabled || collider.gameObject.layer != 6)
            return;

        moveSpeed *= -1;
        transform.localScale = new Vector2(-transform.localScale.x, 1f);
    }
}