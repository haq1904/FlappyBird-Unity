using DG.Tweening;
using UnityEngine;

public class ChildTwoWay : MonoBehaviour
{
    [SerializeField] TwoWayMovePipe pipe;
    private bool isUp;

    private void OnEnable()
    {       
        isUp = UnityEngine.Random.value > 0.5f;
        RevertDirection();
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    public void RevertDirection()
    {
        if (isUp)
        {
            transform.DOMoveY(pipe.GetUnitYToMove, pipe.GetTimeToMove).SetEase(Ease.OutCubic).SetLink(gameObject);
            isUp = false;
            return;
        }
        transform.DOMoveY(-pipe.GetUnitYToMove, pipe.GetTimeToMove).SetEase(Ease.OutCubic).SetLink(gameObject);
        isUp = true;
    }
}
