using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBubble : MonoBehaviour
{
    private static Player PLAYER;
    private Rigidbody2D rb;
    public Vector2 getRBVelocity() {
        return rb.velocity;
    }
    // Start is called before the first frame update
    void Start()
    {
        PLAYER = Player.player;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 4.0f);
        Debug.Log("Velo: " + rb.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Player p = collider.GetComponent<Player>();
        Debug.Log("is collider null: " + (collider == null));
        Debug.Log("is p PLAYER: " + (p == PLAYER));
        if (collider != null && p != PLAYER) {
            Debug.Log("destroyed");
            PLAYER.setUsingLargeBubble(false);
            Destroy(this.gameObject);
        }
    }
}
