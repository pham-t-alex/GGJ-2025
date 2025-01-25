using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p != null)
        {
            p.PickupBubble();
            Destroy(transform.parent.gameObject);
        }
    }
}
