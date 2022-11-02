using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float attackDamage;
    private int _enemyLayer;
    private int _destructableLayer;

    private void Start() {
        _enemyLayer = LayerMask.NameToLayer("Enemy");
        _destructableLayer = LayerMask.NameToLayer("Destructable");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == _enemyLayer) {
            other.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        if (other.gameObject.layer == _destructableLayer) {
            other.GetComponent<Destructable>().HitDestructable();
        }
    }
}
