using UnityEngine;

public class AimLineController : MonoBehaviour
{
    [SerializeField] private Transform _catThrowTransform;
    [SerializeField] private Transform _bottomTransform;

    private LineRenderer _lineRenderer;

    private float _topPos;
    private float _bottomPos;
    private float _x;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        _x = _catThrowTransform.position.x;
        _topPos = _catThrowTransform.position.y;
        _bottomPos = _bottomTransform.position.y;

        _lineRenderer.SetPosition(0, new Vector3(_x, _topPos));
        _lineRenderer.SetPosition(1, new Vector3(_x, _bottomPos));

    }
    private void OnValidate()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _x = _catThrowTransform.position.x;
        _topPos = _catThrowTransform.position.y;
        _bottomPos = _bottomTransform.position.y;

        _lineRenderer.SetPosition(0, new Vector3(_x, _topPos));
        _lineRenderer.SetPosition(1, new Vector3(_x, _bottomPos));
    }
}
