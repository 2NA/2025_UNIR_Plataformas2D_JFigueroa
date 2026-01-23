using UnityEngine;

public class HitBox2D : MonoBehaviour
{
    [SerializeField] string affectedTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(affectedTag))
        {
            // Debug.Log(affectedTag);
            MovementController movementController = collision.GetComponent<MovementController>();
            movementController.NotifyHit(this);
        }
    }
}
