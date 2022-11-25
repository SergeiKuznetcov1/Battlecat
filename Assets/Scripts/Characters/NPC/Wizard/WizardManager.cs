using UnityEngine;

public class WizardManager : MonoBehaviour
{
	[SerializeField] private GameObject _wizard; 
	[SerializeField] private GameObject _explosionVFX;
    private Transform _dialoguePos;
    private Vector3 _wizardScale;

    private void Start() {
        _wizardScale = _wizard.transform.localScale;
    }

    public void StartDisappearSequence() {
        GameObject explosion = Instantiate(_explosionVFX, _dialoguePos.position, Quaternion.identity, transform);
        Destroy(explosion, 1.0f);
        Invoke(nameof(DisenableWizard), 0.5f);
    }

    public void StartAppearSequence(Transform dialoguePos) {
        _dialoguePos = dialoguePos;
        GameObject explosion = Instantiate(_explosionVFX, _dialoguePos.position, Quaternion.identity, transform);
        explosion.GetComponentInChildren<Animator>().SetTrigger("Appear");
        Destroy(explosion, 1.0f);
        Invoke(nameof(EnableWizard), 0.5f);
    }

    private void DisenableWizard() {
        _wizard.SetActive(false);
        _wizard.transform.localScale = _wizardScale;
        _wizard.GetComponentInChildren<WizardLookDir>()._wizardLooking = WizardLookDir.Direction.Left;
    }

    private void EnableWizard() {
        _wizard.SetActive(true);
        _wizard.transform.position = _dialoguePos.position;
    }
}
