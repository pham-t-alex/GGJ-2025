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
    private float jumping = 0;
    [SerializeField] private float attackRange = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            jumping = 0.1f;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject g = Instantiate(ObjectController.Instance.AttackArea, transform.position + Vector3.Normalize((Vector3)worldPos - transform.position) * attackRange, Quaternion.identity);
            Destroy(g, 0.1f);
        }
    }

    public void ThrowSmallBubble(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject g = Instantiate(ObjectController.Instance.DistractionBubble, transform.position, Quaternion.identity);
            g.GetComponent<DistractionBubble>().Launch(Vector3.Normalize((Vector3)worldPos - transform.position));
            Destroy(g, 10f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.layer == 3)
        {
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

    public void PickupBubble()
    {
        
    }
}
