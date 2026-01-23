using UnityEngine;

public class EnemyWithSight : EnemyController
{
    Sight2D sight2D;

    protected override void Awake()
    {
        base.Awake();
        
        sight2D = GetComponent<Sight2D>();
    }

    protected override void Update()
    {
        DetectPlayer();

        base.Update();
    }
    
    private void DetectPlayer()
    {
        bool playerDetected = sight2D.isPlayerInSight();
        
        if (playerDetected)
        {
            RunToPlayer();
        } 
        else
        {
            desiredMove = Vector2.zero;
        }
    }
    protected virtual void RunToPlayer()
    {
        if (player.position.x < transform.position.x)
        {
            desiredMove = Vector2.left;
        }
        else
        {
            desiredMove = Vector2.right;
        }
    }
}
