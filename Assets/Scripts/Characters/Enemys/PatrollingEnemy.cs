using UnityEngine;

public class PatrollingEnemy : Enemy
{
    public float speed;
    private int _lookDirection = -1;
    private bool _detectGround;
    private bool _detectWall;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask layerToCheck;
    public float radius;
	private void FixedUpdate() {
        Flip();
        _rb.velocity = new Vector2(_lookDirection * speed, _rb.velocity.y);
    }

    public override void HurtSequence()
    {
        _anim.SetTrigger("Hurt");
    }

    public override void DeathSequence()
    {
        _anim.SetTrigger("Death");
        speed = 0.0f;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponentInChildren<PolygonCollider2D>().enabled = false;
        _rb.gravityScale = 0.0f;
    }

    private void Flip() {
        _detectGround = Physics2D.OverlapCircle(groundCheck.position, radius, layerToCheck);
        _detectWall = Physics2D.OverlapCircle(wallCheck.position, radius, layerToCheck);

        if (_detectWall || _detectGround == false) {
            _lookDirection *= -1;
            transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
        Gizmos.DrawWireSphere(wallCheck.position, radius);
    }
}
