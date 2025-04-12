using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SlimeVisual : MonoBehaviour
{
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private EnemyEntity _enemyEntity;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private const string ATTACK = "Attack";
    public const string TAKE_HIT = "TakeHit";
    private const string IS_DIE = "IsDie";

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Start() {
        _enemyAI.OnEnemyAttack += _enemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit += _enemyEntity_OnTakeHit;
        _enemyEntity.OnDeath += _enemyEntity_OnDeath;
    }

    // private void TriggerAttackAnimationTurnOnOff() {
    //     TriggerAttackAnimationTurnOn();
    //     TriggerAttackAnimationTurnOff();
    // }
    
    public void TriggerAttackAnimationTurnOff() {
        _enemyEntity.PoligonColliderTurnOff();
    }
    public void TriggerAttackAnimationTurnOn() {
        _enemyEntity.PoligonColliderTurnOn();
    }
    private void OnDestroy() {
        _enemyAI.OnEnemyAttack -= _enemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit -= _enemyEntity_OnTakeHit;
        _enemyEntity.OnDeath -= _enemyEntity_OnDeath;
    } 
    private void _enemyAI_OnEnemyAttack(object sender, EventArgs e) {
        _animator.SetTrigger(ATTACK);
    }
    private void _enemyEntity_OnTakeHit(object sender, EventArgs e) {
        _animator.SetTrigger(TAKE_HIT);
    }
    private void _enemyEntity_OnDeath (object sender, EventArgs e) {
        OnDestroy();
        _animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = -1;
    }    
}

