using UnityEngine;

public class Destructable : MonoBehaviour
{
	public int hitsToDestroy;
    protected Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void HitDestructable() {
        hitsToDestroy -= 1;
        Hit();
        if (hitsToDestroy <= 0 ) {
            Destroy();
        }
    }

    public void CleanUp() {
        Destroy(gameObject);
    }

    public virtual void Hit() {

    }

    public virtual void Destroy() {

    }
}
