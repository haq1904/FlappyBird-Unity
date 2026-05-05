using UnityEngine;

public class BasicPipe : BasePipe
{
    private void OnEnable()
    {
        float randSpawnHeight = UnityEngine.Random.Range(heightRangeBot, heightRangeTop);
        transform.position = new Vector3(transform.position.x, randSpawnHeight, transform.position.z);
    }
}
