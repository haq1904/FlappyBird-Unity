using Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    [SerializeField] private float globalShakeForce =  0.3f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }
}

