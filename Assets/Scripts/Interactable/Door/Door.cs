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
            GetComponent<SpriteRenderer>().sprite = unlockedDoorSprite; 
            _boxCol.enabled = false;
            other.GetComponent<GatherInput>().DisableControls();
            PlayerMoveControls pMC = other.GetComponent<PlayerMoveControls>();
            pMC.reachedDoor = true;
            PlayerStats playerStats = other.GetComponentInChildren<PlayerStats>();
            playerStats.GetComponentInChildren<PolygonCollider2D>().enabled = false;
            print(playerStats.GetComponentInChildren<PolygonCollider2D>().enabled = false);
            PlayerPrefs.SetFloat("HealthKey", playerStats.health);

            PlayerCollectibles collectibles = other.GetComponent<PlayerCollectibles>();
            PlayerPrefs.SetInt("GemNumber", collectibles.gemNumber);

            GameManager.ManagerLoadLevel(lvlToLoad);
        }
    }
    /*
    public void UnlockDoor() {
        GetComponent<SpriteRenderer>().sprite = unlockedDoorSprite; 
        _boxCol.enabled = true;
    }
    */
}
