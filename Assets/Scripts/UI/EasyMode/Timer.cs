using DG.Tweening;
using Unity.VisualScripting;
using Unity.XR.OpenVR;
using UnityEditor.Rendering;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject[] birds;
    

    public void OnEnable()
    {
        var sequence = DOTween.Sequence();

        sequence.AppendInterval(1f);


        for (int i = 0; i< birds.Length; i++)
        {
            float randX = UnityEngine.Random.Range(-4, 4);
            float randY = UnityEngine.Random.Range(2, 4);
            int index = i;
            Rigidbody2D rb = birds[index].GetComponent<Rigidbody2D>();
            sequence.AppendCallback(() =>
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(new Vector2(randX, randY), ForceMode2D.Impulse);
                rb.angularVelocity = 200f;
            });
            sequence.AppendInterval(1f);
        }

    }



}
