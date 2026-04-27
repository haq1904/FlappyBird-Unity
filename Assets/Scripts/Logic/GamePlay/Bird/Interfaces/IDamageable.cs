using UnityEngine;

public enum DamageableAnimationStandard
{
    HitTopGroud,
    HitBotGround,
    HitPipe,
}

public interface IDamageable
{ 
    void PlayAnimation(DamageableAnimationStandard animationName);

    void TakeDamage(Vector2 vector2,float impactForce,float gravityScale);


}
