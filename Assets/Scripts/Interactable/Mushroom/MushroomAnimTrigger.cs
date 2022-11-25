using UnityEngine;

public class MushroomAnimTrigger : MonoBehaviour
{
    [SerializeField] private Animator _mushroomAnim;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Stats")) {
            // PlayerMoveControls playerMove = other.GetComponentInParent<PlayerMoveControls>();
            // playerMove.KnockUp(forceY);
            _mushroomAnim.SetTrigger("PushUp");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Stats"))
            _mushroomAnim.ResetTrigger("PushUp");
    }
}
