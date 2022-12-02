using UnityEngine;

public class Patrol : MonoBehaviour
{
	[SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _fire;
    private Vector2 _target;
    private int _curTargetIndex;

    private void Start() {
        _target = _points[_curTargetIndex].position;
    }

    private void Update() {
        // if (Mathf.Approximately(transform.position.x, _target.x)) {
        //     _curTargetIndex = ++_curTargetIndex % _points.Length;
        //     _target = _points[_curTargetIndex].position;
        //     transform.localScale = new Vector3(transform.localScale.x * -1,
        //                                         transform.localScale.y,
        //                                         transform.localScale.z); 
        // }
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    public void ActivateFire() {
        _fire.SetActive(true);
    }
}
