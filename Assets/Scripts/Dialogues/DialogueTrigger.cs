using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Dialogue Dialogue;
    public DialogueManager DialogueManager;

    private bool _triggered;
    
    public void TriggerDialogue () {
        _triggered = true;
        DialogueManager.StartConversation(Dialogue, transform);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && _triggered == false)
        {
            _triggered = true;
            TriggerDialogue();
            DialogueManager.DialogueEnded = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && DialogueManager.DialogueEnded == false) {
            DialogueManager.EndDialogue();
        }
    }
}
