using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Base))]
public class ResourceScanner : MonoBehaviour
{
    private Base _base;
    private bool _isScanning = true;
    private int _minResourcesAmount = 1;
    private float _scanInterval = 0.1f;
    private Coroutine _scanArea;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    private void OnDestroy()
    {
        StopScanningArea();
    }

    private void OnDisable()
    {
        StopScanningArea();
    }

    private void OnEnable()
    {
        StopScanningArea();
        _scanArea = StartCoroutine(ScanArea());
    }

    private void StopScanningArea()
    {
        if (_scanArea != null)
            StopCoroutine(_scanArea);
    }

    private IEnumerator ScanArea()
    {
        var waitForSeconds = new WaitForSeconds(_scanInterval);
        List<Resource> allResources = new List<Resource>();
        List<Resource> resources = new List<Resource>();

        while (_isScanning)
        {
            yield return waitForSeconds;
            allResources = (FindObjectsOfType<Resource>().ToList());

            if (allResources.Count >= _minResourcesAmount)
            {
                foreach (var resource in allResources)
                {
                    if(resource.CheckCanCollected())
                    {
                        resource.AppointCollector();
                        resources.Add(resource);
                        _base.SearchFreeCollector(resource);
                        break;
                    }
                }
            }

            resources.Clear();
            allResources.Clear();
        }
    }
}