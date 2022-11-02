using UnityEngine;

public class Box : Destructable
{
    public override void Hit()
    {
        _animator.SetTrigger("Hit");
    }

    public override void Destroy()
    {
        _animator.SetTrigger("Destroy");
    }
}
