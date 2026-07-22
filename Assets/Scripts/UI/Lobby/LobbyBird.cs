using DG.Tweening;
using UnityEngine;

public class LobbyBird : MonoBehaviour
{
    [Header("JumpAnchorPos fiels")]
    [SerializeField] private float _jumpPower = 1;
    [SerializeField] private int _numJumps = 1;
    [SerializeField] private float _duration = 1;

    [Header("Fields")]
    [SerializeField] private Animator _animator;

    public void HandleCollision()
    {
        _animator.enabled = false;
        float targetX = transform.localPosition.x - 800f;
        transform.DOLocalJump(new Vector3(targetX, -2300, transform.localPosition.z), _jumpPower, _numJumps, _duration);
    }
}
