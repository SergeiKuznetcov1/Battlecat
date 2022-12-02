using UnityEngine;
using Pathfinding;

public class LookAtPlayer : MonoBehaviour
{
	public AIPath aiPath;
    private float _scaleValue;

    private void Start() {
        _scaleValue = transform.localScale.x;
    }

    private void Update() {
        if (aiPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(-_scaleValue, _scaleValue, _scaleValue);
        } 
        else if (aiPath.desiredVelocity.x <= 0.01f) {
            transform.localScale = new Vector3(_scaleValue, _scaleValue, _scaleValue);
        }
    }
}
