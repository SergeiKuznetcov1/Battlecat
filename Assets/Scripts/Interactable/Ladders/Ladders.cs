using UnityEngine;

public class Ladders : MonoBehaviour
{
	private GatherInput _gI;
    private PlayerMoveControls _pMC;

    private void OnTriggerEnter2D(Collider2D other) {
        _gI = other.GetComponent<GatherInput>();
        _pMC = other.GetComponent<PlayerMoveControls>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (_gI.tryToClimb) {
            _pMC.onLadders = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        _pMC.ExitLadders();
    }
}
