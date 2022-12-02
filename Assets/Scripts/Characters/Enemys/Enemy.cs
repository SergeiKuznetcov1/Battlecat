using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float health;
    protected Rigidbody2D _rb;
    protected Animator _anim;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            DeathSequence();
        }
        HurtSequence();
    }

    public virtual void HurtSequence() {

    }

    public virtual void DeathSequence() {

    }     
}
