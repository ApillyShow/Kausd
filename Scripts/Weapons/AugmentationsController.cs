using UnityEngine;
using static Augmentations;

public class AugmentationsController : MonoBehaviour {
//     [SerializeField] private GameObject _PiercingHitPrefab;
//     [SerializeField] private Transform _player;
// //    private int spawnDistance = 2;
    [SerializeField] private SwordController _baseAttack;
    
    readonly Augmentation piercingHit = new BaseAttack();
    readonly Augmentation baseAttack = new BaseAttack();

    public SwordController BaseAttack() {
        return _baseAttack;
    }
}
