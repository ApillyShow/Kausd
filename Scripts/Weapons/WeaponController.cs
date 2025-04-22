using UnityEngine;

public class WeaponController : MonoBehaviour {
    [SerializeField] private Camera _camera;
    public LayerMask enemyLayer;
    public float detectionRadius = 10f; 
    private Vector3 _mousePos;
    private Transform _currentTargetEnemy; // Текущий целевой враг
    private bool _autoAiming = true;

    private void Update() {
        // if (SwordController.Instacne.IsAttacking() == false && _autoAiming != true) {
        //     MousePosition();
        // }
        FindClosestEnemy();
        MousePosition();
        //SwitchAim();
    }

    private void SwitchAim() {
        if (Input.GetKeyDown(KeyCode.C)) {
            _autoAiming = !_autoAiming;
            if (_autoAiming){
                AutoAiming();
                Debug.Log("Автоприцел включен");
            } else {
                MousePosition();
                Debug.Log("Автоприцел выключен");
            }
        }
    }
    private void MousePosition() {
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(_mousePos.y - transform.position.y, _mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        transform.localRotation = Quaternion.Euler(0, 0,angle);
    }

    private void AutoAiming() {        
        if (_currentTargetEnemy != null) {
            Vector3 direction = _currentTargetEnemy.position - transform.position; 
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    private void FindClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        float closestDistance = Mathf.Infinity;
        _currentTargetEnemy = null;

    foreach (Collider2D enemy in enemies) {
        float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                _currentTargetEnemy = enemy.transform;
            }
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
