public class Augmentations {
    
    public abstract class Augmentation {
        
        public AugmentationsController _augmentationsController;
        public abstract void Execute();

    }
    public class BaseAttack : Augmentation {
        public override void Execute() {
        _augmentationsController.GetActiveAugmentation();
        }
    }

    public class PiercingHit : Augmentation {
        public override void Execute() {
        }
    }
}