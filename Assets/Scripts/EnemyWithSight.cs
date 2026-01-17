using UnityEngine;

public class EnemyWithSight : EnemyController
{
    Sight2D sight2D;

    protected override void Awake()
    {
        base.Awake();
        
        sight2D = GetComponent<Sight2D>();
    }

    protected override void RunToPlayer()
    {
        bool playerDetected = sight2D.isPlayerInSight();
        
        if (playerDetected)
        {
            base.RunToPlayer();
        } 
        else
        {
            desiredMove = Vector2.zero;
        }
    }
}
