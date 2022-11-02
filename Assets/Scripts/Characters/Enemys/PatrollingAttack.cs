using UnityEngine;

public class PatrollingAttack : EnemyAttack
{
    private PlayerMoveControls _playerMoveControl; 
    public float forceX;
    public float forceY;
    public float duration;
    public override void SpecialAttack()
    {
        _playerMoveControl = _playerStats.GetComponentInParent<PlayerMoveControls>();
        StartCoroutine(_playerMoveControl.KnockBackCO(forceX, forceY, duration, transform.parent));
    }
}
