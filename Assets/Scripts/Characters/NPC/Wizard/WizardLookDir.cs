using UnityEngine;

public class WizardLookDir : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    public enum Direction {
        Left,
        Right
    }

    public Direction _wizardLooking;
    private Direction _playerOutFrom;

    private void Start() {
        _wizardLooking = Direction.Left;
    }

	private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (_playerTransform.position.x < transform.position.x) 
                _playerOutFrom = Direction.Left;
            else 
                _playerOutFrom = Direction.Right;
            if (_wizardLooking != _playerOutFrom) {
                transform.localScale = new Vector3(transform.localScale.x * -1, 
                                                    transform.localScale.y,
                                                    transform.localScale.z);
                _wizardLooking = _playerOutFrom;
            }
        }
    }
}
