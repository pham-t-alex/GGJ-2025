using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class GroundedSideToSideMoveBehavior : EnemyMoveBehavior
{
    [SerializeField] private float leftBound = 0;
    [SerializeField] private float rightBound = 0;
    [SerializeField] private float waitTimeAtEdge = 0;
    [SerializeField] private bool rightward = false;
    private float paused = 0;
    private Rigidbody2D rb;
    private Collider2D col;
    [SerializeField] private float speed = 3;
    private bool grounded = false;
    private float preDistractedView;
    private bool preDistractedOrientation;
    
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void Initialize(float leftBound, float rightBound, float waitTimeAtEdge, bool rightward, float speed)
    {
        this.leftBound = leftBound;
        this.rightBound = rightBound;
        this.waitTimeAtEdge = waitTimeAtEdge;
        this.rightward = rightward;
        this.speed = speed;
    }

    private void Update()
    {
        if (paused > 0)
        {
            paused -= Time.deltaTime;
            if (paused <= 0)
            {
                rightward = !rightward;
                enemy.FlipView();
                paused = 0;
            }
        }
    }

    protected override void StandardMovement()
    {
        if (grounded && paused == 0)
        {
            if ((rightward && transform.position.x >= rightBound) || (!rightward && transform.position.x <= leftBound))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                paused = waitTimeAtEdge;
                return;
            }
            Bounds bounds = col.bounds;
            int sign = rightward ? 1 : -1;
            Vector2 enemyEdge = (Vector2)bounds.center + new Vector2(sign * bounds.extents.x, 0);
            int layerMask = 1 << 3;
            RaycastHit2D hit = Physics2D.Raycast(enemyEdge, Vector2.down, bounds.extents.y + 0.1f, layerMask);

            if (hit)
            {
                //Debug.Log("moving");
                rb.velocity = new Vector2(sign * speed, rb.velocity.y);
                //Debug.Log(rb.velocity);
            }

            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                paused = waitTimeAtEdge;
            }
        }
    }

    protected override void DistractedMovement()
    {
        if (grounded)
        {
            if (enemy.DistractionBubble.transform.position.x > transform.position.x)
            {
                rightward = true;
            }
            else if (enemy.DistractionBubble.transform.position.x < transform.position.x)
            {
                rightward = false;
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                return;
            }

            Bounds bounds = col.bounds;
            int sign = rightward ? 1 : -1;
            Vector2 enemyEdge = (Vector2)bounds.center + new Vector2(sign * bounds.extents.x, 0);
            int layerMask = 1 << 3;
            RaycastHit2D hit = Physics2D.Raycast(enemyEdge, Vector2.down, bounds.extents.y + 0.1f, layerMask);

            if (hit)
            {
                rb.velocity = new Vector2(sign * speed, rb.velocity.y);
            }

            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    protected override void DistractedUpdate(float view)
    {
        preDistractedOrientation = rightward;
        preDistractedView = view;
    }

    protected override void NotDistractedUpdate()
    {
        enemy.RestoreView(preDistractedView);
        if (preDistractedOrientation != rightward)
        {
            enemy.FlipView();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = false;
            
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector3 normal = collision.GetContact(i).normal;
                if (normal == Vector3.up)
                {
                    grounded = true;
                }

                else if (normal == Vector3.right)
                {
                    if (!rightward)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        if (paused == 0)
                        {
                            paused = waitTimeAtEdge;
                        }
                        Debug.Log("block");
                    }
                }

                else if (normal == Vector3.left)
                {
                    if (rightward)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        if (paused == 0)
                        {
                            paused = waitTimeAtEdge;
                        }
                        Debug.Log("block");
                    }
                }
            }
        }
    }
}