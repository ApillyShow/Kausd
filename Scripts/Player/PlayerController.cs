using System;
using System.Collections;
using TMPro;
using UnityEngine;

[SelectionBase] // При нажатии выбирать героя самого
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    public static PlayerController Instance {get; private set;}
    [SerializeField] private ValueSystem _healthSystem = new();
    [SerializeField] private ExpSystem _expSystem = new();
    [SerializeField] private TextMeshProUGUI Level;
    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _damageRecoveryTime = 1.5f;    
    [SerializeField] private float _speed = 1f;
    
    public EventHandler OnPlayerDeath;
    private Rigidbody2D _rb2D;
    private Vector2 _movement;
    private Vector2 _playerInput;

    public bool IsRunning() => _isRunning; // Публичный метод. Работает как return _isRunning
    public bool IsAlive() => _isAlive;
    public int _currentLevel;

    private int _expValue;
    private bool _isAlive;
    private bool _canTakeDamage;
    private readonly float minMoveingSpeed;
    private bool _isRunning;
    

    private void Awake() {
        Instance = this;
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
        _isAlive = true;
        _healthSystem.Setup(100);
        StartCoroutine(BaseRegeneration());
        _expSystem._level = _currentLevel;
        _canTakeDamage = true;
    }
    private void Update() {
        _playerInput = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate() {
        MovementControll();
        CurrentLevel();
        CrurrentHP();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Exp")) {

            _expValue = UnityEngine.Random.Range(1,3);
            _expSystem.AddExpValue(_expValue);
        }
    }
    public Vector3 GetPlayerScreenPosition() {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    public void TakeDamage(int damage) {
        if (_canTakeDamage && _isAlive) {
            _canTakeDamage = false;
            _healthSystem.RemoveValue(damage);
            StartCoroutine(DamageRecoveryRoutime());
        }
        DetectDeath();
    }

    private IEnumerator DamageRecoveryRoutime() {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }
    private IEnumerator BaseRegeneration() {
        while (true) {
            _healthSystem.AddValue(0.5f);
            yield return new WaitForSeconds(2);
            if (IsAlive() == false) {
                yield break;
            }
        }
    } 
    private void MovementControll() {
        _movement = _playerInput * _speed;
        _rb2D.MovePosition(_rb2D.position + _movement * Time.deltaTime);

        if (Mathf.Abs(_playerInput.x) > minMoveingSpeed || Mathf.Abs(_playerInput.y) > minMoveingSpeed) {
            _isRunning = true;
        } else {
            _isRunning = false;
        }
    }
    private void GameInput_OnPlayerAttack(object sender, EventArgs e) {  // событие связанное с GameInput
        SwordController.Instacne.Attack();
        //ActiveWeapon.Instacne.GetActiveWeapon().Attack();
    }
    
    private void CrurrentHP() {
        HP.text = _healthSystem._value.ToString() + " / " + _healthSystem._valueMax.ToString();
    }
    
    
    private void CurrentLevel() {
        Level.text = _expSystem._level.ToString();
        if (_expSystem._level != _currentLevel) {
            _healthSystem.AddValueMax(2.5f);
            _currentLevel = _expSystem._level;
        }
    }
    private void DetectDeath() {
        if (_healthSystem._value == 0 && _isAlive) {
            _isAlive = false;
            GameInput.Instance.DisableMovement();

            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}

