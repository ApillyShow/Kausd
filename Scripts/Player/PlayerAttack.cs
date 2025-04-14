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
    public void Attack(GameObject hitObject) {
        IsAttacking = true;
        if (hitObject.TryGetComponent<TrailRenderer>(out var trail)) {
            _animator.SetTrigger(ATTACK);
            Vector3 originalPosition = hitObject.transform.position;
            Vector3 newPosition = originalPosition + hitObject.transform.forward * 2f;
                
            trail.Clear();
            hitObject.transform.position = newPosition;
            trail.AddPosition(newPosition);
            _cooldown = _attackSpeed;
        }
    }
}
