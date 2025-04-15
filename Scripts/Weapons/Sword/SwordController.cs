using UnityEngine;

public class SwordController : MonoBehaviour {
    public static SwordController Instacne { get; private set;}
    [SerializeField] private Animator _animator;
    private const string ATTACK = "Attack";
    public float _cooldown;
    public bool IsAttacking() => _isAttacking;
    
    private bool _isAttacking = false;
    private float _attackSpeed = 1f;

    // private int _Level;
    private void Awake() {
        Instacne = this;
        // _Level = PlayerController.Instance._currentLevel;
    }
    private void Update() {
        CooldownSystem();
        //LevelUp();    
    }
    
    // private void LevelUp() {
    //     if (PlayerController.Instance._currentLevel != _Level) {
    //         attack.Damage *= 1.05f;
    //         MathF.Round(attack.Damage, 2);
    //         _Level = PlayerController.Instance._currentLevel;
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out EnemyEntity _)) {
            Attack baseAttack = new BaseAttack() {Damage = 5f};

            baseAttack.Execute();
        }
    }
    public void Attack() {
        _isAttacking = true;
        if (_cooldown <= 0) {
            _animator.SetTrigger(ATTACK);
            _cooldown = _attackSpeed;
        }
    }
    private void CooldownSystem() {
        _cooldown -= Time.deltaTime;
        if (_cooldown <= 0) {
            _cooldown = 0;
            _isAttacking = false;
        }
    }
}
