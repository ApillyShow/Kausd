using UnityEngine;

public class PiercingHitController : MonoBehaviour {
    public static PiercingHitController Instance {get; private set;}
    [SerializeField] private GameObject _piercingHitPrefab;
    [SerializeField] private Transform _spawnPos;
    public float _speed = 15f;

    private void Awake() {
        Instance = this;
    }

    public void Attack() {
        GameObject piercingHit = Instantiate(_piercingHitPrefab, _spawnPos.position, _spawnPos.rotation);
        Rigidbody2D rigidbody2D = piercingHit.GetComponent<Rigidbody2D>();
            
        rigidbody2D.linearVelocity =  _speed * transform.up;  
    }   
}
