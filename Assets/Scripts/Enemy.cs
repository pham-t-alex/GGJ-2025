using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyView enemyView;
    [SerializeField][Range(0, 360)] private float viewAngleCenter = 0f;
    [SerializeField] private float angleWidth = 60f;
    [SerializeField] private Vector2 viewCenter;
    [SerializeField] private float range = 5f;
    [SerializeField] private EnemyMoveBehavior behavior;
    private GameObject distractionBubble;
    public GameObject DistractionBubble
    {
        get
        {
            return distractionBubble;
        }
    }
    public bool Distracted
    {
        get
        {
            return distractionBubble != null;
        }
    }

    public delegate void DistractedDelegate(float viewAngle);
    public event DistractedDelegate DistractedEvent;

    public delegate void NotDistractedDelegate();
    public event NotDistractedDelegate NotDistractedEvent;

    private Rigidbody2D rb;
    private Animator eAnimator;

    public void Initialize(float angleCenter, float angleWidth, Vector2 center, float range)
    {
        viewAngleCenter = angleCenter;
        this.angleWidth = angleWidth;
        viewCenter = center;
        this.range = range;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vision();
        enemyView.Initialize(range, angleWidth);
        enemyView.transform.localPosition = viewCenter;
        enemyView.transform.rotation = Quaternion.Euler(0, 0, viewAngleCenter);
        if (rb.velocity.x != 0) {
            eAnimator.SetBool("Walking", true);
        } else {
            eAnimator.SetBool("Walking", false);
        }
    }

    void Vision()
    {
        CheckIfPlayerInView();
        CheckForDistractions();
        if (Distracted)
        {
            float newAngle = Vector2.SignedAngle(Vector2.right, distractionBubble.transform.position - transform.position);
            viewAngleCenter = (newAngle >= 0) ? newAngle : newAngle + 360;
        }
    }

    void CheckIfPlayerInView()
    {
        if (Player.player == null)
        {
            return;
        }
        // VER 2: PLAYER HITBOX IS JUST CENTER
        Vector2 viewCenterPos = (Vector2)transform.position + viewCenter;
        Vector2 playerPosition = Player.player.transform.position;
        if (Vector2.Distance(viewCenterPos, playerPosition) > range)
        {
            //Debug.Log("too far");
            return;
        }
        float angle = Vector2.Angle(Quaternion.Euler(0, 0, viewAngleCenter) * Vector2.right, playerPosition - viewCenterPos);
        // testing edges of view
        
        if (angle > angleWidth / 2)
        {
            //Debug.Log("outside of view");
            return;
        }
        int layerMask = 1 << 3;
        layerMask |= 1 << 6;
        RaycastHit2D hit = Physics2D.Raycast(viewCenterPos, playerPosition - (Vector2)viewCenterPos, range, layerMask);
        if (hit)
        {
            Player p = hit.collider.GetComponent<Player>();
            if (p != null)
            {
                Destroy(p.gameObject);
                return;
            }
        }

        //Debug.Log("no hit");

        // VER 1: PRECISE PLAYER HITBOX
        /*Vector2 viewCenterPos = (Vector2)transform.position + viewCenter;
        Vector2 playerPosition = Vector2.zero; // REPLACE WHEN DONE WITH PLAYER COLLIDER CENTER
        float playerDiagonalWidth = 1.4f / 2; // REPLACE WITH PLAYER DIAGONAL WIDTH (sqrt BOUNDS.EXTENTS^2)
        if (Vector2.Distance(viewCenterPos, playerPosition) > range + playerDiagonalWidth)
        {
            //Debug.Log("too far");
            return;
        }
        float angle = Vector2.Angle(Quaternion.Euler(0, 0, viewAngleCenter) * Vector2.right, playerPosition - viewCenterPos);
        // testing edges of view
        int layerMask = 1 << 3;
        layerMask |= 1 << 6;

        // check edges of angle bounding 
        RaycastHit2D hit1 = Physics2D.Raycast(viewCenterPos, Quaternion.Euler(0, 0, viewAngleCenter - (angleWidth / 2)) * Vector2.right, range, layerMask);
        if (hit1 && hit1.collider.gameObject.name == "Player") // REPLACE WITH .GetComponent<Player>()
        {
            //Debug.Log("PLAYER IN VIEW 1");
            return;
            // DO SOMETHING
        }
        RaycastHit2D hit2 = Physics2D.Raycast(viewCenterPos, Quaternion.Euler(0, 0, viewAngleCenter + (angleWidth / 2)) * Vector2.right, range, layerMask);
        if (hit2 && hit2.collider.gameObject.name == "Player") // REPLACE WITH .GetComponent<Player>()
        {
            //Debug.Log("PLAYER IN VIEW 2");
            return;
            // DO SOMETHING
        }
        if (angle > angleWidth / 2)
        {
            //Debug.Log("outside of view");
            return;
        }

        Vector2[] playerPoints = new Vector2[5];
        float xExtents = 0.5f; // REPLACE WITH PLAYER COLLIDER EXTENTS
        float yExtents = 0.5f; // REPLACE WITH PLAYER COLLIDER EXTENTS
        playerPoints[0] = playerPosition;
        playerPoints[1] = playerPosition + new Vector2(-xExtents, -yExtents);
        playerPoints[2] = playerPosition + new Vector2(-xExtents, yExtents);
        playerPoints[3] = playerPosition + new Vector2(xExtents, -yExtents);
        playerPoints[4] = playerPosition + new Vector2(xExtents, yExtents);
        for (int i = 0; i < 4; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(viewCenterPos, playerPoints[i] - (Vector2)viewCenterPos, range, layerMask);
            if (hit)
            {
                if (hit.collider.gameObject.name == "Player") // REPLACE WITH .GetComponent<Player>()
                {
                    //Debug.Log("PLAYER IN VIEW 3");
                    // DO SOMETHING
                    return;
                }
            }
        }

        //Debug.Log("no hit");
        */
    }

    void CheckForDistractions()
    {
        bool wasDistracted = false;
        if (distractionBubble != null)
        {
            wasDistracted = true;
            if (CheckBubbleHelper(distractionBubble))
            {
                return;
            }
        }
        foreach (GameObject b in ObjectController.Instance.DistractionBubbles)
        {
            if (b != distractionBubble && CheckBubbleHelper(b))
            {
                if (!wasDistracted)
                {

                    DistractedEvent?.Invoke(viewAngleCenter);
                }
                distractionBubble = b;
                return;
            }
        }
        distractionBubble = null;
        if (wasDistracted)
        {
            NotDistractedEvent?.Invoke();
        }
    }

    bool CheckBubbleHelper(GameObject b)
    {
        Vector2 viewCenterPos = (Vector2)transform.position + viewCenter;
        Vector2 bubblePos = b.transform.position;
        if (Vector2.Distance(viewCenterPos, bubblePos) > range)
        {
            return false;
        }
        float angle = Vector2.Angle(Quaternion.Euler(0, 0, viewAngleCenter) * Vector2.right, bubblePos - viewCenterPos);
        if (angle > angleWidth / 2)
        {
            return false;
        }
        int layerMask = 1 << 3;
        layerMask |= 1 << 10;
        RaycastHit2D hit = Physics2D.Raycast(viewCenterPos, bubblePos - (Vector2)viewCenterPos, range, layerMask);
        if (hit)
        {
            DistractionBubble bubble = hit.collider.GetComponent<DistractionBubble>();
            if (bubble != null)
            {
                return true; // bubble currently in view
            }
        }
        return false;
    }

    public void FlipView()
    {
        if (viewAngleCenter <= 180)
        {
            viewAngleCenter = 180 - viewAngleCenter;
        }
        else
        {
            viewAngleCenter = 540 - viewAngleCenter;
        }
    }

    public void RestoreView(float view)
    {
        viewAngleCenter = view;
    }

    public void Die()
    {
        Instantiate(ObjectController.Instance.BubbleItem, transform.position, Quaternion.identity);
        eAnimator.SetTrigger("TrDeath");
        Destroy(gameObject);
    }
}
