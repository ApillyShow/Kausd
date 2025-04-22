using UnityEngine;

public class PiercingHitSystem : MonoBehaviour {
    public float _lifetime = 0.1f;
    private float _damage = 10f;

    private void Update() {
        Destroy(gameObject, _lifetime); 
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) {
            enemyEntity.TakeDamage(_damage);
        }
    }
}
