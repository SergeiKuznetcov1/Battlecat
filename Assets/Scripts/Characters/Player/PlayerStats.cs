using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PlayerStats : MonoBehaviour
{
	public float maxHealth;
    public float health;
    public bool canTakeDamage = true;
    public float invulnerabilityTime;
    public Image _healthImage;
    private Animator _animator;
    private PlayerMoveControls _playerMove;
    private PlayerAttackControls _pAC;
    private void Start() {
        _animator = GetComponentInParent<Animator>();
        _playerMove = GetComponentInParent<PlayerMoveControls>();
        _pAC = GetComponentInParent<PlayerAttackControls>();
        health = PlayerPrefs.GetFloat("HealthKey", maxHealth);
        UpdateHealthUI();
    }

    public void TakeDamage(float damage) {
        if (canTakeDamage) {
            health -= damage;
            _animator.SetBool("Damage", true);
            _playerMove.hasControl = false;
            UpdateHealthUI();
            _pAC.ResetAttack();
            if (health <= 0) {
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponentInParent<GatherInput>().DisableControls();
                PlayerPrefs.SetFloat("HealthKey", maxHealth);
                GameManager.ManagerRestartLevel();
            }
        }

        StartCoroutine(DamagePreventionCO());
    }

    IEnumerator DamagePreventionCO() {
        canTakeDamage = false;
        yield return new WaitForSeconds(invulnerabilityTime);
        if (health > 0) {   
            canTakeDamage = true;
            _playerMove.hasControl = true;
            _animator.SetBool("Damage", false);
        }
        else {
            _animator.SetBool("Death", true);
        }
    }

    public void UpdateHealthUI() {
        _healthImage.fillAmount = health / maxHealth;
    }

    public void IncreaseHealth(float healAmount) {
        health += healAmount;
        
        if (health > maxHealth) 
            health = maxHealth;
        
        UpdateHealthUI();
    }

    private void OnApplicationQuit() {
        PlayerPrefs.DeleteKey("HealthKey");
        PlayerPrefs.DeleteKey("GemNumber");    
    }
}
