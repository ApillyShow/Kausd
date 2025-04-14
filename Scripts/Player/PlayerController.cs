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

    private int _sec;
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
        _canTakeDamage = true;
        _isAlive = true;
        _healthSystem.Setup(100);
        _expSystem._level = _currentLevel;
        _sec = _timer.sec;
    }
    private void Update() {
        _playerInput = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate() {
        MovementControll();
        CurrentLevel();
        CrurrentHP();
        if (_timer.sec != _sec) {
            BaseRegeneration();
        }
        if(_isAlive) {
            Regeneration();
        }
    }

    public bool IsRunning() => _isRunning; // Публичный метод. Работает как return _isRunning
    public bool IsAlive() => _isAlive;

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

    private void MovementControll() {
        _movement = _playerInput * _speed;
        _rb2D.MovePosition(_rb2D.position + _movement * Time.deltaTime);

        if (Mathf.Abs(_playerInput.x) > minMoveingSpeed || Mathf.Abs(_playerInput.y) > minMoveingSpeed) {
            _isRunning = true;
        } else {
            _isRunning = false;
        }
    }
    private IEnumerator DamageRecoveryRoutime() {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }
    private void GameInput_OnPlayerAttack(object sender, EventArgs e) {  // событие связанное с GameInput
        PlayerAttack.Instacne.Attack();
        //ActiveWeapon.Instacne.GetActiveWeapon().Attack();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Exp")) {

            _expValue = UnityEngine.Random.Range(1,3);
            _expSystem.AddExpValue(_expValue);
        }
    }
    
    private void CrurrentHP() {
        HP.text = _healthSystem._value.ToString() + " / " + _healthSystem._valueMax.ToString();
    }
    
    private void BaseRegeneration() {
        _healthSystem.AddValue(0.3f);
    } 
    
    private void CurrentLevel() {
        Level.text = _expSystem._level.ToString();
        if (_expSystem._level != _currentLevel) {
            _healthSystem.AddValueMax(1.02f);
            _currentLevel = _expSystem._level;
        }
    }

    private void Regeneration() {
        _healthSystem.AddValue(0.3f * Time.deltaTime);
    }
    private void DetectDeath() {
        if (_healthSystem._value == 0 && _isAlive) {
            _isAlive = false;
            GameInput.Instance.DisableMovement();

            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}

