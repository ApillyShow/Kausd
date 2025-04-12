using UnityEngine;

public class WeaponController : MonoBehaviour {
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerAttack _playerAttack;
    private Vector3 _mousePos;
    private void Update() {
        if (_playerAttack.IsAttacking == false) {
            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(_mousePos.y - transform.position.y, _mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            transform.localRotation = Quaternion.Euler(0, 0,angle);
        }
    }
}
