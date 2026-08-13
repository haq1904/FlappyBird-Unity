using UnityEngine;

public class UIPrewarmFix : MonoBehaviour
{
    [SerializeField] private float prewarmTime = 50f; // Thời gian muốn tua nhanh

    private void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        if (ps != null)
        {
            // Tua nhanh thời gian tới tương lai (50 giây sau)
            ps.Simulate(prewarmTime, true, true);
            // Sau khi tới tương lai thì bấm Play cho nó chạy tiếp bình thường
            ps.Play();
        }
    }
}

