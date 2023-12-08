using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private float _respawnInterval;
    [SerializeField] private Resource _resource;

    private float _minAxisValue = -10;
    private float _maxAxisValue = 10;
    private float _resourcePositionY = 0.8f;
    private bool _isCreating = true;
    private Coroutine _createResource;

    private void Start()
    {
        _createResource = StartCoroutine(CreateResource());
    }

    private void OnDestroy()
    {
        if (_createResource != null)
            StopCoroutine(_createResource);
    }

    private Vector3 SetSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(_minAxisValue, _maxAxisValue), 
            _resourcePositionY, transform.position.z + Random.Range(_minAxisValue, _maxAxisValue));
        return spawnPosition;
    }

    private IEnumerator CreateResource()
    {
        var waitForSeconds = new WaitForSeconds(_respawnInterval);

        while (_isCreating)
        {
            yield return waitForSeconds;
            Instantiate(_resource, SetSpawnPosition(), Quaternion.identity);
        }
    }
}