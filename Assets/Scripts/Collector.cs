using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Resource _currentResource;
    private Base _baseObject;
    private Vector3 _basePosition;
    private Vector3 _currentFlagPosition;
    private bool _isGoForResource;
    private bool _isGoToBase;
    private bool _isGoBuildNewBase;

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

        if (_isGoBuildNewBase)
        {
            GoBuildNewBase();
        }
    }

    public void Init(Base baseObject)
    {
        _baseObject = baseObject;
        _basePosition = baseObject.transform.position;
        transform.position = _basePosition;
    }

    public void AppointResource(Resource resource)
    {
        IsBusyCollecting = true;
        _isGoForResource = true;
        _currentResource = resource;
    }

    public void InitBuildBase(Vector3 currentFlagPosition)
    {
        _currentFlagPosition = currentFlagPosition;
        _isGoBuildNewBase = true;
        _isGoForResource = false;
    }

    private void GoBuildNewBase()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentFlagPosition,
            _speed * Time.deltaTime);

        if (transform.position == _currentFlagPosition)
        {
            _isGoBuildNewBase = false;
            IsBusyCollecting = false;
            _baseObject.FinishBuildNewBase();
            Base newBase = Instantiate(_baseObject, _currentFlagPosition, Quaternion.identity);
            Init(newBase);
            newBase.AddNewCollector(this);
        }
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
            Destroy(_currentResource.gameObject);
        }
    }
}