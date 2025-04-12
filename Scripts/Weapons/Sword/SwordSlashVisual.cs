using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordSlashVisual : MonoBehaviour {
    [SerializeField] private PlayerAttack sword;

    private Animator animator;
    private const string ATTACK = "Attack";
    
    private void Awake() {
        animator = GetComponent<Animator>(); 
    }
    private void Sword_OnSwordSwing(object sender, System.EventArgs e) {
        animator.SetTrigger(ATTACK);
    }
}
