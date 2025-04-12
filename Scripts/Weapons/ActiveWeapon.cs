using UnityEngine;

public class ActiveWeapon : MonoBehaviour {
    public static ActiveWeapon Instacne { get; private set;}
    [SerializeField] private PlayerAttack sword;

    private void Awake() {
        Instacne = this;
    }
    private void Update() {
        if (PlayerController.Instance.IsAlive()) 
        FollowMousePosition();
    }
    public PlayerAttack GetActiveWeapon() {
        return sword;
    }

    public void FollowMousePosition() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = PlayerController.Instance.GetPlayerScreenPosition();

        if(mousePos.x < playerPos.x) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }    
}
