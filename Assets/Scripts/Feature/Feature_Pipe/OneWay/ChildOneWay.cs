using DG.Tweening;
using UnityEngine;

public class ChildOneWay : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private OneWayMovePipe parentPipe;
    [SerializeField] private float timeToMove = 1f;
    private float targetY;

    private void OnEnable()
    {
        float startRange = 0;
        float endRange = 0;

        if (parentPipe.GetRandSpawnHeight>=0)
        {
            startRange = -parentPipe.GetHeightRange - parentPipe.GetRandSpawnHeight;
            endRange = parentPipe.GetRandSpawnHeight - parentPipe.GetUnitToMove;
        }
        else
        {
            startRange = parentPipe.GetRandSpawnHeight + parentPipe.GetUnitToMove;
            endRange = parentPipe.GetHeightRange - parentPipe.GetRandSpawnHeight;
        }
        
        targetY = UnityEngine.Random.Range(startRange, endRange);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }


    public void MoveUpDown()
    {
        transform.DOKill();
        transform.DOLocalMoveY(targetY, timeToMove).SetUpdate(UpdateType.Fixed).SetEase(Ease.OutCubic).SetLink(gameObject);
    }

}
