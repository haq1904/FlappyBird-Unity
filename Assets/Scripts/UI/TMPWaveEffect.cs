using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TMPWaveEffect : MonoBehaviour
{
    [Header("Wave Settings")]
    public float waveSpeed = 5f;      // Tốc độ lượn sóng (Nhanh/Chậm)
    public float waveHeight = 10f;    // Độ cao của sóng (Nhấp nhô mạnh/yếu)
    public float waveFrequency = 1f;  // Độ giãn của sóng (Khoảng cách giữa các nhịp)

    private TMP_Text _textComponent;

    void Awake()
    {
        _textComponent = GetComponent<TMP_Text>();
    }

    void Update()
    {
        // Yêu cầu TMP update lại mesh trước khi ta biến tấu nó
        _textComponent.ForceMeshUpdate();
        var textInfo = _textComponent.textInfo;

        // Quét qua từng chữ cái một
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            // Bỏ qua khoảng trắng (dấu cách)
            if (!charInfo.isVisible) continue;

            // Lấy danh sách các đỉnh (vertices) của chữ cái này
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            // Mỗi chữ cái có 4 đỉnh (tạo thành 1 hình chữ nhật)
            for (int j = 0; j < 4; j++)
            {
                var orig = verts[charInfo.vertexIndex + j];
                // Dùng hàm Sin để bẻ cao độ (Y) tạo đường lượn sóng
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.unscaledTime * waveSpeed + i * waveFrequency) * waveHeight, 0);
            }
        }

        // Đẩy dữ liệu mới vào lại Mesh để vẽ ra màn hình
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            _textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
