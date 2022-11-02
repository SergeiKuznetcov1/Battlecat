using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	public float speed;
    public int startingPoint;
    public Transform[] points;
    private int _pointIndex;

    private void Start() {
        transform.position = points[startingPoint].position;
    }

    private void Update() {
        if (Vector2.Distance(transform.position, points[_pointIndex].position) < 0.01f) {
            _pointIndex++;
            if (_pointIndex == points.Length) {
                _pointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[_pointIndex].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Player")) {
            if (transform.position.y < other.transform.position.y -0.8f) 
                other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.collider.CompareTag("Player")) {
            other.transform.SetParent(null);
        }
     }
}
