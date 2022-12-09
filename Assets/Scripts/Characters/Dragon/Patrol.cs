using UnityEngine;
using System;

public class Patrol : MonoBehaviour
{
    enum DragonState {
        Idle,
        Attack
    }

    enum DragonDirection {
        Right,
        Left
    }
    [SerializeField] private Transform[] _idlePoints;
	[SerializeField] private Transform[] _idlePointsReverset;
	[SerializeField] private Transform[] _attackPoints;
	[SerializeField] private Transform[] _attackPointsReverset;
	[SerializeField] private Transform _leftPoint;
	[SerializeField] private Transform _rightPoint;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _fire;
    private Animator _animator;
    private Vector2 _target;
    private int _curTargetIndex;
    private DragonState _curState;
    private DragonDirection _curDir;
    private bool _doFireStrike;
    private float _checkedPoint;
    public bool DragonDead;
    private void Start() {
        _curTargetIndex = 0;
        _curState = DragonState.Idle;
        _curDir = DragonDirection.Left;
        _animator = GetComponent<Animator>();
        _target = _idlePoints[_curTargetIndex].position;
    }
    private void Update() {     
        if (DragonDead) {
            _fire.SetActive(false);
            return;   
        }
        if (transform.position.x ==_leftPoint.position.x) {
                _curDir = DragonDirection.Right;
                FlipDragon();
            } else if (transform.position.x == _rightPoint.position.x) {
                _curDir = DragonDirection.Left;
                FlipDragon();
            }

        if (_curState == DragonState.Idle) {
            _animator.SetTrigger("Idle");
            if (transform.position.x == _target.x) {
                CheckForFireStrike();
                if (_doFireStrike) {
                    _curState = DragonState.Attack;
                    _curTargetIndex = 0;
                    return;
                }
                _curTargetIndex = ++_curTargetIndex % _idlePoints.Length;
                _target = _idlePoints[_curTargetIndex].position;
            }
        }

        if (_curState == DragonState.Attack) {
            _animator.SetTrigger("FireStrike");
            if (transform.position.x == _target.x) {
                if (_curTargetIndex == _attackPoints.Length - 1) {
                    _curState = DragonState.Idle;
                    _curTargetIndex = 0;
                    _doFireStrike = false;
                    return;
                }
                _curTargetIndex = ++_curTargetIndex % _attackPoints.Length;
                if (_curDir == DragonDirection.Right)
                    _target = _attackPoints[_curTargetIndex].position;
                else if (_curDir == DragonDirection.Left)
                    _target = _attackPointsReverset[_curTargetIndex].position;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }
    private void FlipDragon() {
        if (_curDir == DragonDirection.Right) {
            transform.localScale = new Vector3(-1.0f,
                                                transform.localScale.y,
                                                transform.localScale.z); 
        } else if (_curDir == DragonDirection.Left) {
            transform.localScale = new Vector3(1.0f,
                                    transform.localScale.y,
                                    transform.localScale.z); 
        }
    }

    private void CheckForFireStrike() {
        System.Random random = new System.Random();
        int value = random.Next(0, 2);
        _doFireStrike = value == 1 ? true : false;
    }

    // called through animation FireStrike, object Dragon
    public void ActivateFire() {
        _fire.SetActive(true);
    }

    // called through animation Idle, object Dragon
    public void TurnOffFire() {
        _fire.SetActive(false);
    }
}
