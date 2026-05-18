using UnityEngine;

public class PickupObj : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
    
        if (other.transform.CompareTag("Player"))
        {
        LevelManager.PickupCount += 1;
            Destroy(gameObject);
        }
    }
  
}
