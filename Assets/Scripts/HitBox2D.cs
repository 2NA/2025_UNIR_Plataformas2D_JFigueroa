using UnityEngine;

public class HitBox2D : MonoBehaviour
{
    [SerializeField] string affectedTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) 
        {
            Debug.LogWarning("Collision null en HitBox2D");
            return;
        }
        
        GameObject obj = collision.gameObject;
        if (obj == null) return;
        
        if (collision.CompareTag(affectedTag))
        {
            Debug.Log(affectedTag);

            // Intenta MovementController primero
            if (collision.TryGetComponent<MovementController>(out var movement))
            {
                movement.NotifyHit(this);
            }
            // Luego ItemController
            else if (collision.TryGetComponent<ItemController>(out var item))
            {
                item.NotifyHit(this);
            }
        }
    }

}
