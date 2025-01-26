using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool moving = false;
    private bool movedRightLast = true;
    [SerializeField] private float jumpStrength = 0f;
    [SerializeField] private float speed = 0.0f;
    private int maxJumps = 1;
    private int jumpCount = 1;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;
    private Rigidbody2D originalRB;
    private float jumping = 0;

    private static Player _player;
    public static Player player
    {
        get
        {
            if (_player == null)
            {
                _player = FindObjectOfType<Player>();
            }
            return _player;
        }
    }

    [SerializeField] private GameObject LargeBubble;
    private GameObject newLargeBubble;
    private bool usingLargeBubble = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalRB = rb;
        _player = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumping > 0)
        {
            jumping -= Time.deltaTime;
            if (jumping < 0)
            {
                jumping = 0;
            }
        }
        if (newLargeBubble != null && usingLargeBubble) {
            rb.velocity = new Vector2(moveDirection.x * speed, newLargeBubble.GetComponent<Rigidbody2D>().velocity.y);
            newLargeBubble.transform.position = this.transform.position;
        } else {
            rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);    
        }
    }
    /*
    private void FixedUpdate() {
        if (newLargeBubble != null && usingLargeBubble) {
            rb.velocity = new Vector2(moveDirection.x * speed, newLargeBubble.GetComponent<Rigidbody2D>().velocity.y);
            newLargeBubble.transform.position = this.transform.position;
        } else {
            rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);    
        }
        /*
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
        if (newLargeBubble != null && usingLargeBubble) {
            this.transform.position = new Vector2(newLargeBubble.transform.position.x, newLargeBubble.transform.position.y);
            //rb = newLargeBubble.GetComponent<Rigidbody2D>();
        }
        
    }
    */

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
            jumping = 0.1f;
        }
    }
    private void OnCollisionStay2D(Collision2D collision) {
        Debug.Log("collision.gameObject.layer: " + collision.gameObject.layer);
        if (collision.gameObject.layer == 0/*3*/)
        {
            Debug.Log("collision.contactCount: " + collision.contactCount);
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (collision.GetContact(i).normal == Vector2.up && jumping == 0)
                {
                    jumpCount = maxJumps;
                    Debug.Log("Jumps reset");
                }
            }
        }
    }
    public void UseLargeBubble(InputAction.CallbackContext context) {
        Debug.Log("pressed number 2");
        if (context.started) {
            newLargeBubble = Instantiate(LargeBubble);
            newLargeBubble.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            //Debug.Log("newLargeBubble position: " + newLargeBubble.transform.position);
            //rb = newLargeBubble.GetComponent<Rigidbody2D>();
            usingLargeBubble = true;
        }
    }
    public void setUsingLargeBubble(bool other) {
        usingLargeBubble = other;
    }
}
