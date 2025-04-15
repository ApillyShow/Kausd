using UnityEngine;
using System;

public class BatVisual : MonoBehaviour {
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private EnemyEntity _enemyEntity;
    [SerializeField] private GameObject _shadow;

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
        _enemyAI.OnEnemyAttack += EnemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit += EnemyEntity_OnTakeHit;
        _enemyEntity.OnDeath += EnemyEntity_OnDeath;
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
    private void EnemyAI_OnEnemyAttack(object sender, EventArgs e) {
        _animator.SetTrigger(ATTACK);
    }
    private void EnemyEntity_OnTakeHit(object sender, EventArgs e) {
        _animator.SetTrigger(TAKE_HIT);
    }
    private void EnemyEntity_OnDeath (object sender, EventArgs e) {
        OnDestroy();
        _animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = -1;
        _shadow.SetActive(false);
    } 
    private void OnDestroy() {
        _enemyAI.OnEnemyAttack -= EnemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit -= EnemyEntity_OnTakeHit;
        _enemyEntity.OnDeath -= EnemyEntity_OnDeath;
    } 
}
