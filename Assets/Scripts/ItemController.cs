using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private string itemType = "Coin";
    
    public virtual void NotifyHit(HitBox2D hitBox2D)
    {
        PlayerController player = hitBox2D.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            player.CollectItem(itemType);
            Destroy(gameObject);
        }
    }
}
