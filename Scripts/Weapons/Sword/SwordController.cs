using System;
using UnityEngine;

public class SwordController : MonoBehaviour {

    [SerializeField] private float _damageAmount = 5;
    private int _Level;
    private void Awake() {
        _Level = PlayerController.Instance._currentLevel;
    }
    private void Update() {
        LevelUp();    
    }
    
    private void LevelUp() {
        if (PlayerController.Instance._currentLevel != _Level) {
            _damageAmount *= 1.05f;
            MathF.Round(_damageAmount, 2);
            _Level = PlayerController.Instance._currentLevel;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) {
            enemyEntity.TakeDamage(_damageAmount);
        }
    }
}
