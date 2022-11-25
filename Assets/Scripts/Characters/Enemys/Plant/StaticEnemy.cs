using UnityEngine;

public class StaticEnemy : Enemy
{
    [SerializeField] private Transform _bulletSP;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private PolygonCollider2D _attackCollider;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _pushX;
    [SerializeField] private float _pushY;
    [SerializeField] private float _pushZ;
    [SerializeField] private float _deathDelay;
    public override void HurtSequence()
    {
        _anim.SetTrigger("PlantHurt");
    }
    public override void DeathSequence()
    {
        _anim.SetTrigger("PlantDeath");
        _attackCollider.enabled = false;
        _capsuleCollider.enabled = false;
        _rigidBody.constraints = RigidbodyConstraints2D.None;
        _rigidBody.AddForce(new Vector2(Random.Range(_pushX, _pushX + _pushX),
                            Random.Range(_pushY, _pushY + _pushY)),
                            ForceMode2D.Impulse);
        _rigidBody.AddTorque(Random.Range(_pushZ, _pushZ + _pushZ), ForceMode2D.Impulse);
        Destroy(gameObject, _deathDelay);
    }

    // called through PlantAttack animation, StaticRangeAttack object
    public void SpawnBullet() {
        GameObject bullet = Instantiate(_bulletPrefab, _bulletSP.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * _bulletSpeed, ForceMode2D.Impulse);
        GetComponent<AudioSource>().Play();
    }
}
