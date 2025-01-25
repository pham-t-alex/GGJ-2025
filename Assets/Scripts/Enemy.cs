using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyView enemyView;
    [SerializeField][Range(0, 360)] private float viewAngleCenter = 0f;
    [SerializeField] private float angleWidth = 60f;
    [SerializeField] private Vector2 viewCenter;
    [SerializeField] private float range = 5f;
    // Start is called before the first frame update
    void Start()
    {
        enemyView.Initialize(range, angleWidth);
    }

    // Update is called once per frame
    void Update()
    {
        enemyView.transform.localPosition = viewCenter;
        enemyView.transform.rotation = Quaternion.Euler(0, 0, viewAngleCenter);
        CheckIfPlayerInView();
    }

    void CheckIfPlayerInView()
    {
        Vector2 playerPosition = Vector2.zero; // REPLACE WHEN DONE WITH PLAYER
        if (Vector2.Distance(transform.position, playerPosition) > range)
        {
            Debug.Log("too far");
            return;
        }
        float angle = Vector2.Angle(Quaternion.Euler(0, 0, viewAngleCenter) * Vector2.right, playerPosition - (Vector2)transform.position);
        if (angle > angleWidth / 2)
        {
            Debug.Log("outside of view");
            return;
        }
        int layerMask = 1 << 3;
        layerMask |= 1 << 6;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerPosition - (Vector2)transform.position, range, layerMask);
        if (hit)
        {
            if (hit.collider.gameObject.name == "Player") // REPLACE WITH .GetComponent<Player>()
            {
                Debug.Log("PLAYER IN VIEW");
                // DO SOMETHING
            }
            else
            {
                Debug.Log("obstructed");
            }
        }
        Debug.Log("no hit");

        // devise alternative solution using raycasts and the player's body
        // also center it at viewCenter + transform position instead of transform position
    }
}
