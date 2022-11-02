using UnityEngine;
using TMPro;

public class PlayerCollectibles : MonoBehaviour
{
    public TextMeshProUGUI textGemNumber;
	public int gemNumber;

    private void Start() {
        gemNumber = PlayerPrefs.GetInt("GemNumber", 0);
        UpdateText();
    }

    private void UpdateText() {
        textGemNumber.text = gemNumber.ToString();
    }

    public void GemCollected() {
        gemNumber++;
        UpdateText();
    }
}
