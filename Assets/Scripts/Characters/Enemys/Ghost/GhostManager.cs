using UnityEngine;
using Pathfinding;
public class GhostManager : MonoBehaviour
{
	[SerializeField] private Transform _ghostIdlePoint;
    [SerializeField] private AIDestinationSetter _ghostDestination;

    private void GhostChasePlayer(Transform player) {
        _ghostDestination.target = player;
    }

    private void GhostIdle() {
        _ghostDestination.target = _ghostIdlePoint;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
            GhostChasePlayer(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
            GhostIdle();
    }
}
