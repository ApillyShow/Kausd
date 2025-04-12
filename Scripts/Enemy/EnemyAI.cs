using System;
using UnityEngine;
using UnityEngine.AI;
using Vaylos.Utils;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour {
   
    // agent state
    [SerializeField] private State _startingState; 
    private NavMeshAgent _navMeshAgent;
    private State _currentState; // текущее состяние агента(нпс)

    // direction
    
    private Vector3 _lastPosition;
    private float _nextCheckDirectionTime = 0f;
    private float _checkDirectionDuration = 0.1f;

    public bool IsRunning {
        get {
            if (_navMeshAgent.velocity == Vector3.zero) {
                return false;
            } else {
                return true;
            }
        }
    }    

    // Attacking 
    [SerializeField] private float _attackingDistance = 2f;
    [SerializeField] private float _attackRate = 2f;
    public event EventHandler OnEnemyAttack;
    private float _nextAttackTime = 0f;

    private enum State {
        Chasing,  
        Attacing,
        Death
    }
    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _currentState = _startingState;
    }

    private void Update() {
        StateHander();
        MovementDirectionHandler();
    }
    public void SetDeathState() {
        _navMeshAgent.ResetPath();
        _currentState = State.Death;
    }
    private void StateHander() {
        switch (_currentState) {
            default: 
            case State.Chasing:
                ChasingTarget();
                ChekCurrentState();
                break;
            case State.Attacing:
                AttackingTarget();
                ChekCurrentState();
                break;
            case State.Death:
                break;
        }
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition) {
        if (sourcePosition.x > targetPosition.x) {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void ChasingTarget() {
        _navMeshAgent.SetDestination(PlayerController.Instance.transform.position);
    }

    private void AttackingTarget() {
        if (Time.time > _nextAttackTime) {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            
            _nextAttackTime = Time.time + _attackRate;
        }
    }

    private void MovementDirectionHandler() {
        if (Time.time > _nextCheckDirectionTime) {
            if (IsRunning) {
                ChangeFacingDirection(_lastPosition, transform.position);
            } else if (_currentState == State.Attacing) {
                ChangeFacingDirection(transform.position, PlayerController.Instance.transform.position);
            }
            _lastPosition = transform.position;
            _nextCheckDirectionTime = Time.time + _checkDirectionDuration;
        }
    }
 
    private void ChekCurrentState() {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);
        State newstate = State.Chasing;

        if (distanceToPlayer <= _attackingDistance) {
            newstate = State.Attacing;
        }

        if (newstate != _currentState) {
            if (newstate == State.Chasing) {
                _navMeshAgent.ResetPath();

            } else if (newstate == State.Attacing) {
                _navMeshAgent.ResetPath();
            }
             
            _currentState = newstate;
        }
    }
}
