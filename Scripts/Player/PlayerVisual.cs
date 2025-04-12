using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerVisual : MonoBehaviour {
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private const string IS_RUNNING = "IsRunning";
    private const string IS_DIE = "IsDie";
    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        PlayerController.Instance.OnPlayerDeath += Player_OnPlayerDeath;   
    }
    private void Update() {
        animator.SetBool(IS_RUNNING, PlayerController.Instance.IsRunning());
        
        if(PlayerController.Instance.IsAlive())
        AdjustPlayerFacingDirection();
    }

    public void AdjustPlayerFacingDirection() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = PlayerController.Instance.GetPlayerScreenPosition();

        if(mousePos.x < playerPos.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e) {
        animator.SetBool(IS_DIE, true);
    }
}
