using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionBubble : MonoBehaviour
{
    [SerializeField] private float launchForce;
    // Start is called before the first frame update
    void Start()
    {
        ObjectController.Instance.DistractionBubbles.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * launchForce);
        Debug.Log(direction * launchForce);
    }

    private void OnDestroy()
    {
        ObjectController.Instance.DistractionBubbles.Remove(gameObject);
    }
}
