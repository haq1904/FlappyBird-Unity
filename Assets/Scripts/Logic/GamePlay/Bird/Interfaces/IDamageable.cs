using UnityEngine;

public enum AnimationStandard
{
    HitTopGroud,
    HitBotGround,
    HitPipe,
}

public interface IDamageable
{ 
    void PlayAnimation(AnimationStandard animation);//Play animation

    void TakeDamage(Vector2 vector2,float impactForce,float gravityScale);


}
