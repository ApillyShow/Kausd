using UnityEngine;

public class SwordController : MonoBehaviour {
    
    [SerializeField] private int _damageAmount = 5;
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) {
            enemyEntity.TakeDamage(_damageAmount);
        }
    }
}
