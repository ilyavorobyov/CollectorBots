using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Resource _currentResource;
    private Vector3 _basePosition;
    private bool _isGoForResource;
    private bool _isGoToBase;

    public bool IsBusyCollecting { get; private set; } = false;

    private void Update()
    {
        if (_isGoForResource)
        {
            GoForResource();
        }
        if (_isGoToBase)
        {
            GoBaseWithResource();
        }
    }

    public void Init(Vector3 basePosition)
    {
        _basePosition = basePosition;
        transform.position = _basePosition;
    }

    public void AppointResource(Resource resource)
    {
        IsBusyCollecting = true;
        _isGoForResource = true;
        _currentResource = resource;
    }

    private void GoForResource()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentResource.transform.position,
            _speed * Time.deltaTime);

        if (transform.position == _currentResource.transform.position)
        {
            _isGoForResource = false;
            _isGoToBase = true;
        }
    }

    private void GoBaseWithResource()
    {
        transform.position = Vector3.MoveTowards(transform.position, _basePosition,
            _speed * Time.deltaTime);
        _currentResource.transform.position = transform.position;

        if (transform.position == _basePosition)
        {
            IsBusyCollecting = false;
            _isGoToBase = false;
        }
    }
}