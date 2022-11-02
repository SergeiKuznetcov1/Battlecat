using UnityEngine;

public class Spike : MonoBehaviour
{
	public float damage;
    public float forceX;
    public float forceY;
    public float duration;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Stats")) {
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            PlayerMoveControls playerMove = other.GetComponentInParent<PlayerMoveControls>();
            StartCoroutine(playerMove.KnockBackCO(forceX, forceY, duration, transform));
        }
    }
}
