using UnityEngine;

public class HearthPickup : MonoBehaviour
{
	public float heal;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerStats playerStats = other.GetComponentInChildren<PlayerStats>();

            if (playerStats.health == playerStats.maxHealth) 
                return;

            playerStats.IncreaseHealth(heal);
            Destroy(gameObject);
        }
    }
}
