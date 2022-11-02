using UnityEngine;

public class PlayerAttackControls : MonoBehaviour
{
	private PlayerMoveControls _pMC;
    private GatherInput _gI;
    private Animator _animator;
    public bool attackStarted;
    public PolygonCollider2D polyCol;
    public AudioSource audioSource;

    private void Start() {
        _pMC = GetComponent<PlayerMoveControls>();
        _gI = GetComponent<GatherInput>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        Attack();
    }

    private void Attack() {
        if (_gI.tryAttack) {
            if (attackStarted || _pMC.hasControl == false || _pMC.knockBack || _pMC.onLadders || _pMC.crouch)
                return;
            _animator.SetBool("Attack", true);
            attackStarted = true;
        }
    }
    // called through animation Attack, object Player
    public void ActivateAttack() {
        polyCol.enabled = true;
        audioSource.Play();
    }
    // called through animation Attack, object Player
    public void ResetAttack() {
        _animator.SetBool("Attack", false);
        _gI.tryAttack = false;
        attackStarted = false;
        polyCol.enabled = false;
        audioSource.Stop();
    }
}
