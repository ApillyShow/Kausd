using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyAI))]
public class EnemyEntity : MonoBehaviour {
    public static EnemyEntity Instance {get; private set;}
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private GameObject _expPrefab;
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;
    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private EnemyAI _enemyAI;
    private float _currentHealth;


    private void Awake() {
        Instance = this;
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }
    private void Start() {
        _currentHealth = _enemySO.enemyHealth;
    }
    
    
    public void PoligonColliderTurnOff() {
        _polygonCollider2D.enabled = false;
    }

    public void PoligonColliderTurnOn() {
        _polygonCollider2D.enabled = true;
    }

    public void TakeDamage(float damage) {
        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);

        DetectDeath();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out PlayerController player)) {
            player.TakeDamage(_enemySO.enemyDamageAmount);
        }
    }
    private void DetectDeath() {
        if (_currentHealth <= 0) {
            //_boxCollider2D.enabled = false;
            //polygonCollider2D.enabled = false;
            _enemyAI.SetDeathState();
            OnDeath?.Invoke(this, EventArgs.Empty);
            Instantiate(_expPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject, 2f);
        }
    }
}