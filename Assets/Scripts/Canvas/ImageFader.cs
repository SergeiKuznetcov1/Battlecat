using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageFader : MonoBehaviour
{
	private Animator _anim;
    private int _lvlToLoad;

    private void Start() {
        _anim = GetComponent<Animator>();
        GameManager.RegisterImageFader(this);
    }

    public void SetLevel(int lvl) {
        _lvlToLoad = lvl;
        _anim.SetTrigger("Fade");
    }

    // called through "FadeOut" animation event, ImageFader object 
    public void LoadLevel() {
        SceneManager.LoadScene(_lvlToLoad);
    }

    public void RestartLevel()
    {
        Invoke(nameof(Restart), 1.0f);
    }

    private void Restart() {
        SetLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
