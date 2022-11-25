using UnityEngine;

public class MushroomPushUp : MonoBehaviour
{
	[SerializeField] private PlayerMoveControls _playerMC;

    [SerializeField] private float forceY;
    public void PushUp() {
        _playerMC.KnockUp(forceY);
        GetComponent<AudioSource>().Play();
    }
}
