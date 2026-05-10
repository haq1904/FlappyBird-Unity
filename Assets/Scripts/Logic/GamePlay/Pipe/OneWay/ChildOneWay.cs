using DG.Tweening;
using UnityEngine;

public class ChildOneWay : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private BasePipe parentPipe;
    [SerializeField] private float heightRangeTop = 3f;
    [SerializeField] private float heightRangeBot = -2.5f;
    [SerializeField] private float timeToMove = 1f;
    [SerializeField] private float distanceToMove = 1f;
    private float targetY;

    private void OnEnable()
    { 
        do
        {
            targetY = UnityEngine.Random.Range(heightRangeBot, heightRangeTop);
        } while ((Mathf.Abs(targetY) < distanceToMove));
    }


    public void MoveUpDown()
    { 
        transform.DOLocalMoveY(targetY, timeToMove).SetUpdate(UpdateType.Fixed).SetEase(Ease.OutCubic).SetLink(gameObject);
    }

    public void Stop()
    {
        transform.DOKill(false);
    }
}
