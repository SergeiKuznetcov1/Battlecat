using UnityEngine;

public class GhostEnemy : Enemy
{
    public override void HurtSequence()
    {
        _anim.SetTrigger("Hurt");
    }

    public override void DeathSequence()
    {
        _anim.SetTrigger("Death");
    }
    // called through animation Death, gameObject Ghost
    public void KillGhost() {
        Destroy(gameObject);
    }
}
