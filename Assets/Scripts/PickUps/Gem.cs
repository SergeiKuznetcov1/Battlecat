using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject gemParticle;
    private void Start() {
        GameManager.RegisterGem(this);
    }
	private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerCollectibles>().GemCollected();
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;

            Instantiate(gemParticle, transform.position, transform.rotation);
            GameManager.RemoveGemFromList(this);
        }
    }
}
