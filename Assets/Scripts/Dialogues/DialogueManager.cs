using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DialogueText;
    public Animator Animator;
    public bool DialogueEnded;
    private Queue<string> _sentences;
    private AudioClip[] _voiceover;
    private AudioSource _audioSource;
    private int _voiceoverCurrent;
    [SerializeField] private GatherInput _pGI;
    [SerializeField] private WizardManager _wizManager;
    private void Start() {
        _sentences = new Queue<string>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (_pGI.nextDialogue && !DialogueEnded)
            DisplayNextSentence();
            _pGI.nextDialogue = false;
    }

    public void StartConversation(Dialogue dialogue, Transform converPos) {
        _wizManager.StartAppearSequence(converPos);
        _voiceover = dialogue.Voiceover;
        Animator.SetBool("IsOpen", true);
        NameText.text = dialogue.name;
        DialogueText.text = "";
        _sentences.Clear();

        foreach (string sentence in dialogue.Sentences) {
            _sentences.Enqueue(sentence);
        }

        Invoke(nameof(DisplayNextSentence), 0.5f); 
    }

    public void DisplayNextSentence() {
        if (_sentences.Count == 0) {
            EndDialogue();
            return;
        }
        PlayVoiceover();
        string sentence = _sentences.Dequeue();
        StopCoroutine(StartCoroutine(TypeSentence(sentence)));
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            DialogueText.text += letter;
            yield return null;
        }
    }

    private void PlayVoiceover() {
        _voiceoverCurrent = _voiceoverCurrent % _voiceover.Length;
        StopVoiceover();
        _audioSource.PlayOneShot(_voiceover[_voiceoverCurrent]);
        _voiceoverCurrent++;
    }

    private void StopVoiceover() {
        if (_audioSource.isPlaying)
            _audioSource.Stop();
    }

    public void EndDialogue() {
        _wizManager.StartDisappearSequence();
        DialogueEnded = true;
        StopVoiceover();
        Animator.SetBool("IsOpen", false);
    }
}
