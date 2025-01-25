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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyView.Initialize(range, angleWidth);
        enemyView.transform.localPosition = viewCenter;
        enemyView.transform.rotation = Quaternion.Euler(0, 0, viewAngleCenter);
        CheckIfPlayerInView();
    }

    void CheckIfPlayerInView()
    {
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
                Debug.Log("PLAYER IN VIEW");
                // DO SOMETHING
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
}
