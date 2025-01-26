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
        enemy.DistractedEvent += DistractedUpdate;
        enemy.NotDistractedEvent += NotDistractedUpdate;
    }

    protected abstract void StandardMovement();
    protected abstract void DistractedMovement();

    private void FixedUpdate()
    {
        if (enemy.Distracted)
        {
            DistractedMovement();
        }
        else
        {
            StandardMovement();
        }
    }

    protected virtual void DistractedUpdate(float view)
    {

    }
    protected virtual void NotDistractedUpdate()
    {

    }
}
