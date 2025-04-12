using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SkeletonVisual : MonoBehaviour {
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private EnemyEntity _enemyEntity;
    [SerializeField] private GameObject _shadow;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private const string TAKE_HIT = "TakeHit";
    private const string ATTACK = "Attack";
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

    // public void TriggerAttackAnimationTurnOnOff() {
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
    private void _enemyEntity_OnTakeHit (object sender, EventArgs e) {
        _animator.SetTrigger(TAKE_HIT);
    }
    private void _enemyEntity_OnDeath (object sender, EventArgs e) {
        _animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = -2;
        _shadow.SetActive(false);
    }    
}
