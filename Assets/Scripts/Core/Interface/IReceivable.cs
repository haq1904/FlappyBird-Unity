public enum ReceivableAnimationStandard
{

}

public interface IReceivable
{
    public void PlayAnimation(ReceivableAnimationStandard animationName);
    public void AddPoint(float point);
}