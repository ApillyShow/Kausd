using System;
using UnityEngine;

public class SwordController : MonoBehaviour {
    public static SwordController Instacne { get; private set;}
    [SerializeField] private GameObject _piercingHitController;
    [SerializeField] private Animator _animator;

    private const string ATTACK = "Attack";
    public float _cooldown;
    public bool IsAttacking() => _isAttacking;

    private bool _isAttacking = false;
    private float _attackSpeed = 1f;
    private float _damage = 100f;
    private int _Level;

    private void Awake() {
        Instacne = this;
        _Level = PlayerController.Instance._currentLevel;
    }
    private void Update() {
        CooldownSystem();
        IncreasedDamage();    
    }
    
    private void IncreasedDamage() {
        if (PlayerController.Instance._currentLevel != _Level) {
            _damage *= 1.05f;
            MathF.Round(_damage, 2);
            _Level = PlayerController.Instance._currentLevel;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) {
            enemyEntity.TakeDamage(_damage);
        }
    }
    public void Attack() {
        _isAttacking = true;
        if (_cooldown <= 0) {
            _animator.SetTrigger(ATTACK);
            _cooldown = _attackSpeed;
            if (_piercingHitController.activeInHierarchy == true) {
                PiercingHitController.Instance.Attack();
            }
            _isAttacking = false;
        }
    }
    private void CooldownSystem() {
        _cooldown -= Time.deltaTime;
        if (_cooldown <= 0) {
            _cooldown = 0;
        }
    }
}
