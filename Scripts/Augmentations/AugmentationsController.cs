using UnityEngine;
public class AugmentationsController : MonoBehaviour {
    
    // Piercing Hit
    [SerializeField] private GameObject piercingHitController;
    
    public void PiercingSwordHit() {
        if (piercingHitController.activeInHierarchy == false) {
            piercingHitController.SetActive(true);
        }
    }

    // new Augmetation
}
