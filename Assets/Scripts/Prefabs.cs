using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    private static Prefabs instance;
    public static Prefabs Instance
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
