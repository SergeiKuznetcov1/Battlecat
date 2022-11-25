using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Stats")) {
            Destroy(gameObject);
        }
    }
}
