using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerEnemyGSS : SpawnerEnemyMovement
{
    [SerializeField] private float leftBound = 0;
    [SerializeField] private float rightBound = 0;
    [SerializeField] private float waitTimeAtEdge = 0;
    [SerializeField] private bool rightward = false;
    [SerializeField] private float speed = 3;
    public override void InitializeEnemyMovement(Enemy e)
    {
        e.AddComponent<GroundedSideToSideMoveBehavior>();
        e.GetComponent<GroundedSideToSideMoveBehavior>().Initialize(leftBound, rightBound, waitTimeAtEdge, rightward, speed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
