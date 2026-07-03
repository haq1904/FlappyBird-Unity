


using UnityEngine;

public enum DamageableAnimationStandard
{

}

public interface IDamageable
{
    public void TakeDamage(Vector2 direction, float impactForce, float gravityScale);

    public void PlayAnimation(DamageableAnimationStandard animation);
}