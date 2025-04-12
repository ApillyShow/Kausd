using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    
    public static PlayerAttack Instacne { get; private set;}
    [SerializeField] private float _attackSpeed;
    [SerializeField] private Animator _animator;
    private const string ATTACK = "Attack";
    public float _cooldown;

    private void Awake() {
        Instacne = this;
    }

    private void Update() {
        _cooldown -= Time.deltaTime;
        if (_cooldown <= 0) {
            _cooldown = 0;
        }
    }

    public void Attack() {
        if (_cooldown <= 0) {
            _animator.SetTrigger(ATTACK);
            _cooldown = _attackSpeed;
        }
    }
}
