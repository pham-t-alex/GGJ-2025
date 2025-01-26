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
    private Animator bAnimator;
    // Start is called before the first frame update
    void Start()
    {
        PLAYER = Player.player;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 3.0f);
        Debug.Log("Velo: " + rb.velocity);
        bAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.GetContact(0).normal != Vector2.up) {
            Pop();
        } 
    }
    /*
    private void OnTriggerEnter2D(Collider2D collider) {
        Player p = collider.GetComponent<Player>();
        Debug.Log("is collider null: " + (collider == null));
        Debug.Log("is p PLAYER: " + (p == PLAYER));
        if (collider != null && p != PLAYER) {
            Pop();
        }
    }
    */

    public void Pop()
    {
        Debug.Log("destroyed");
        bAnimator.SetTrigger("TrBigBubbleShot");
        PLAYER.setUsingLargeBubble(false);
        Destroy(this.gameObject);
    }
}
