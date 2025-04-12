using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ExpController : MonoBehaviour {
    public static ExpController Instance {get; private set;}
    private GameObject _player;
    private float _distanceToPlayer;
    private readonly float _speed = 5.5f;
    private readonly float _radius = 2.5f;

    private void Awake() {
        Instance = this;
        _player = GameObject.FindWithTag("Player");
    }
    private void Update() {
        FindPlayer();
    }

    public void FindPlayer() {
        _distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
         if (_player != null && _radius >= _distanceToPlayer) {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            if (_distanceToPlayer < 0.2) {
                Destroy(gameObject);
            }
        }
    }
}