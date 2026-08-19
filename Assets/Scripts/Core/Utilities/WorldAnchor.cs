using UnityEngine;

public class WorldAnchor : MonoBehaviour
{
    public enum AnchorType
    {
        Center,
        Top,
        Bottom,
        Left,
        Right,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    [Header("Anchor Settings")]
    [Tooltip("Choose the edge you want to anchor to")]
    [SerializeField] private AnchorType _anchorType = AnchorType.Center;

    [Tooltip("Additional offset adjustmen")]
    [SerializeField] private Vector3 _offset;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        UpdatePosition();
    }


    [ContextMenu("Update Position Now")]
    public void UpdatePosition()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
            if (_mainCamera == null) return;
        }

        Vector3 viewportPos = Vector3.zero;

        // Determine Viewport percentage based on AnchorType
        switch (_anchorType)
        {
            case AnchorType.Center: viewportPos = new Vector3(0.5f, 0.5f, 0); break;
            case AnchorType.Top: viewportPos = new Vector3(0.5f, 1f, 0); break;
            case AnchorType.Bottom: viewportPos = new Vector3(0.5f, 0f, 0); break;
            case AnchorType.Left: viewportPos = new Vector3(0f, 0.5f, 0); break;
            case AnchorType.Right: viewportPos = new Vector3(1f, 0.5f, 0); break;
            case AnchorType.TopLeft: viewportPos = new Vector3(0f, 1f, 0); break;
            case AnchorType.TopRight: viewportPos = new Vector3(1f, 1f, 0); break;
            case AnchorType.BottomLeft: viewportPos = new Vector3(0f, 0f, 0); break;
            case AnchorType.BottomRight: viewportPos = new Vector3(1f, 0f, 0); break;
        }

        // Translate Viewport percentage to World Space coordinates
        Vector3 worldPos = _mainCamera.ViewportToWorldPoint(viewportPos);

        // Keep the original Z axis of the object (do not take the Camera's Z axis)
        worldPos.z = transform.position.z;

        // Apply new position and add offset
        transform.position = worldPos + _offset;
    }
}
