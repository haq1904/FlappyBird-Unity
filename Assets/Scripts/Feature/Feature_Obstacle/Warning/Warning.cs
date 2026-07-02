using System.Xml.Serialization;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private float _followSpeed = 1;

    private IDamageable _targetGameObj;

    private void SetTarget(IDamageable target)
    {
        _targetGameObj = target;
    }

    void Update()
    {
        if (_targetGameObj != null)
        {
            float newY = Mathf.MoveTowards(transform.position.y, _targetGameObj.GetPosition().y, _followSpeed * Time.deltaTime);
            transform.position = new Vector2(transform.position.x, newY);
        }
    }

    private void HandleRestart()
    {
        Destroy(gameObject);
    }

    private void HandleGameOver()
    {
        Destroy(gameObject);
    }
}
