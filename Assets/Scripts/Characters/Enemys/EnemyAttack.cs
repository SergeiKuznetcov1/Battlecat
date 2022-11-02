using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	public float damage;
    protected PlayerStats _playerStats;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Stats")) {
            _playerStats = other.GetComponent<PlayerStats>();
            _playerStats.TakeDamage(damage);

            SpecialAttack();
        }
    }

    public virtual void SpecialAttack() {

    }
}
