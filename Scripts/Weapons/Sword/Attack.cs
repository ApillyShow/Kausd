    public abstract class Attack {
        public float Damage {set; get;}
        public abstract void Execute();
}
public class BaseAttack : Attack {
    public override void Execute() {
        EnemyEntity.Instance.TakeDamage(Damage);
    }
}

public class PiercingHit : Attack {
    public override void Execute() {
        EnemyEntity.Instance.TakeDamage(Damage * 1.5f);
    }
}