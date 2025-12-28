using UnityEngine;

public class EnemyController : MovementController
{    
    protected override void Update()
    {
        desiredMove = Vector2.left;
        
        base.Update();
    }
}
