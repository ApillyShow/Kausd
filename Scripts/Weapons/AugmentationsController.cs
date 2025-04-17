using UnityEngine;
public class AugmentationsController : MonoBehaviour {
    [SerializeField] private GameObject piercingHit;

    public void PiercingSwordHit() {
        if (piercingHit.activeInHierarchy == false) {
            piercingHit.SetActive(true);
        }
    }
}
