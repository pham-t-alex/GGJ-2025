using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 5;
    private float spawnTimer = 0;
    [SerializeField][Range(0, 360)] private float enemyViewAngleCenter = 0f;
    [SerializeField] private float enemyAngleWidth = 60f;
    [SerializeField] private Vector2 viewCenter = new Vector2(0, 0);
    [SerializeField] private float enemyRange = 5f;
    [SerializeField] private SpawnerEnemyMovement enemyMovementBehavior;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        if (spawnTimer <= 0)
        {
            Enemy e = Instantiate(ObjectController.Instance.EnemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            e.Initialize(enemyViewAngleCenter, enemyAngleWidth, viewCenter, enemyRange);
            enemyMovementBehavior.InitializeEnemyMovement(e);
            spawnTimer = spawnInterval;
        }
    }
}
