using UnityEngine;
public class DragonEnemy : Enemy
{
    [SerializeField] private Patrol _dragonPatrol;
    [SerializeField] private Sprite _dFalling;
    [SerializeField] private Sprite _dDead;
    private SpriteRenderer _sR;
    private void Start() {
        _sR = GetComponent<SpriteRenderer>();
    }
    public override void HurtSequence()
    {
        _anim.SetTrigger("Hurt");
    }

    public override void DeathSequence()
    {
        gameObject.layer = LayerMask.NameToLayer("DeadDragon");
        _dragonPatrol.DragonDead = true;
        _anim.enabled = false;
        _rb.gravityScale = 1;
        _sR.sprite = _dFalling;
        GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x, 0.37f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground"))
            _sR.sprite = _dDead;
    }
}
