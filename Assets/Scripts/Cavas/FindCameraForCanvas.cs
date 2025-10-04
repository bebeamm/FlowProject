using UnityEngine;

public class FindCameraForCanvas : MonoBehaviour
{
    private Camera _camera;
    private Canvas _canvas;

    private void Awake()
    {
        FindCanvas();
    }

    private void Update()
    {
        if(_canvas == null)
            FindCanvas();

        if (_camera == null)
            FindCanvas();
    }

    private void FindCanvas()
    {
        _canvas = GetComponent<Canvas>();

        if (_canvas == null)
        {
            FindCanvas();
        }
        else
        {
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            FindCamera();
        }
    }

    private void FindCamera()
    {
        _camera = Camera.main;

        if (_camera == null)
        {
            FindCamera();
        }
        {
            _canvas.worldCamera = _camera;
        }
    }
}
