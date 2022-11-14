using UnityEngine;

public class FallThroughPlatform : MonoBehaviour
{
    private PlayerFallThrough _pFallThrough;
    private PlayerMoveControls _pMC;
    private PlatformEffector2D _platformEffector;

    private void Awake() {
        _platformEffector = GetComponent<PlatformEffector2D>();
    } 

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Player")) {
            _pFallThrough = other.gameObject.GetComponent<PlayerFallThrough>();
            _pMC = other.gameObject.GetComponent<PlayerMoveControls>();
            if (_pMC != null) 
                _pMC.onPlatform = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {

        if (_pFallThrough == null) 
            return;
        if (_pFallThrough.fallThrough) {
            _platformEffector.rotationalOffset = 180;
            _pFallThrough = null;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        _pMC.onPlatform = false;
        _pFallThrough = null;
        _platformEffector.rotationalOffset = 0;
    }
}
