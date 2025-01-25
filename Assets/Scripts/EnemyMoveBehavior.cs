using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public abstract class EnemyMoveBehavior : MonoBehaviour
{
    protected Enemy enemy;

    protected virtual void Start()
    {
        enemy = GetComponent<Enemy>();
    }
}
