using UnityEngine;

abstract public class PlayerService : MonoBehaviour, ITrackable
{
    abstract public Transform GetTransform();
}
