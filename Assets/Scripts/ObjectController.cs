using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private static ObjectController instance;
    public static ObjectController Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] private GameObject attackArea;
    public GameObject AttackArea
    {
        get
        {
            return attackArea;
        }
    }
    [SerializeField] private GameObject bubbleItem;
    public GameObject BubbleItem
    {
        get
        {
            return bubbleItem;
        }
    }
    [SerializeField] private GameObject distractionBubble;
    public GameObject DistractionBubble
    {
        get
        {
            return distractionBubble;
        }
    }

    private HashSet<GameObject> distractionBubbles = new HashSet<GameObject>();
    public HashSet<GameObject> DistractionBubbles
    {
        get
        {
            return distractionBubbles;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
