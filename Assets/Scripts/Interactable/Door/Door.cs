using UnityEngine;
public class Door : MonoBehaviour
{
    public Sprite unlockedDoorSprite;
	public int lvlToLoad;
    private BoxCollider2D _boxCol;
    private void Start() {
        _boxCol = GetComponent<BoxCollider2D>();
        GameManager.RegisterDoor(this);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _boxCol.enabled = false;
            other.GetComponent<GatherInput>().DisableControls();
            PlayerStats playerStats = other.GetComponentInChildren<PlayerStats>();
            PlayerPrefs.SetFloat("HealthKey", playerStats.health);

            PlayerCollectibles collectibles = other.GetComponent<PlayerCollectibles>();
            PlayerPrefs.SetInt("GemNumber", collectibles.gemNumber);

            GameManager.ManagerLoadLevel(lvlToLoad);
        }
    }

    public void UnlockDoor() {
        GetComponent<SpriteRenderer>().sprite = unlockedDoorSprite; 
        _boxCol.enabled = true;
    }
}
