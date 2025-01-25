using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool moving = false;
    private bool movedRightLast = true;
    [SerializeField] private float jumpStrength = 0f;
    [SerializeField] private float speed = 0.0f;
    private int maxJumps = 2;
    private int jumpCount = 0;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //BattleUI.Instance.AddPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        /*
        if (context.canceled)
        {
            if (moving)
            {
                moving = false;
                //InterruptMovement();
            }
            return;
        }
        */
        moving = true;
        Vector2 direction = context.ReadValue<Vector2>();
        moveDirection.x = direction.x;
        if (moveDirection.x < 0)
        {
            //movedLeftLast = true;
            movedRightLast = false;
            //if (player2) GetComponent<SpriteRenderer>().flipX = true;
            //else GetComponent<SpriteRenderer>().flipX = false;
            Debug.Log("Last moved left");
        }
        else if (moveDirection.x > 0)
        {
            //movedLeftLast = false;
            movedRightLast = true;
            //if (!player2) GetComponent<SpriteRenderer>().flipX = true;
            //else GetComponent<SpriteRenderer>().flipX = false;
            Debug.Log("Last moved right");
        }
        /*
        if (WaitingToAct || Busy || Stunned || blocking)
        {
            return;
        }
        */
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && jumpCount > 0)
        {
            jumpCount--;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
            //pAnimator.SetTrigger("TrJump");
            Debug.Log("Jumped!");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.GetContact(0).normal == Vector2.up)
            {
                jumpCount = maxJumps;
                Debug.Log("Jumps reset");
            }
    }
}
