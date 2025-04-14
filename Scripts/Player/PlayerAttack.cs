using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    
    public static PlayerAttack Instacne { get; private set;}
    [SerializeField] private Animator _animator;
    private const string ATTACK = "Attack";
    private float _attackSpeed = 1f;
    public float _cooldown;
    public bool IsAttacking;
    private int _Level;

    private void Awake() {
        Instacne = this;
        _Level = PlayerController.Instance._currentLevel;
    }

    private void Update() {
        CooldownSystem();
        LevelingUp();
    }

    private void CooldownSystem() {
        _cooldown -= Time.deltaTime;
        if (_cooldown <= 0) {
            _cooldown = 0;
            IsAttacking = false;
        }
    }

    public void LevelingUp() {
        if (PlayerController.Instance._currentLevel != _Level) {
            _attackSpeed /= 1.08f;
            MathF.Round(_attackSpeed, 2);
            _Level = PlayerController.Instance._currentLevel;
        }
    }
    public void Attack() {
        IsAttacking = true;
        if (_cooldown <= 0) {
            _animator.SetTrigger(ATTACK);
            _cooldown = _attackSpeed;
        }
    }
}
