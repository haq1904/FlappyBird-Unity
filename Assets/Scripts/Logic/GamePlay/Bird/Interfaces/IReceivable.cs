using UnityEngine;

public enum ReceivableAnimationStandart
{

}

public interface IReceivable
{
    public void PlayAnimation(ReceivableAnimationStandart animationName);

    public void ApplyEffect(float point);

}
