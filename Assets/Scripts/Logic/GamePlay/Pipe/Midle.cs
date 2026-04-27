using UnityEngine;

public class Midle : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IReceivable>(out var receivableGameObject))
        {
            receivableGameObject.ApplyEffect(10);
        }
    }
}
