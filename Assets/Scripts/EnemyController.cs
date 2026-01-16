using System;
using UnityEngine;

public class EnemyController : MovementController
{    
    [SerializeField] float distanceToPunch = 0.25f;
    [SerializeField] float timeBetweenPunches = 1f;
    public Transform player;

    float lastPunchTime;
    protected override void Update()
    {
        if (player.position.x < transform.position.x)
        {
            desiredMove = Vector2.left;
        } else
        {
            desiredMove = Vector2.right;
        }

        if (player.gameObject.activeSelf)
        {
            if (MathF.Abs(player.position.x - transform.position.x) < distanceToPunch)
            {
                desiredMove = Vector2.zero;
                if (Time.time - lastPunchTime > timeBetweenPunches)
                {
                    PerformPunch();
                    lastPunchTime = Time.time;
                }
            }   
        } else
        {
            desiredMove.x *= -1f;
        }      

        base.Update();
    }
}
