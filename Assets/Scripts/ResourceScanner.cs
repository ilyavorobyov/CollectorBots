using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _scanInterval;

    private bool _isScanning = true;
    private int _minResourcesAmount = 1;
    private Coroutine _scanArea;

    private void Start()
    {
        _scanArea = StartCoroutine(ScanArea());
    }
    private void OnDestroy()
    {
        if (_scanArea != null)
            StopCoroutine(_scanArea);
    }

    private IEnumerator ScanArea()
    {
        var waitForSeconds = new WaitForSeconds(_scanInterval);
        List<Resource> allResources;
        List<Resource> resources = new List<Resource>();

        while (_isScanning)
        {
            yield return waitForSeconds;
            allResources = (FindObjectsOfType<Resource>().ToList());

            if (allResources.Count >= _minResourcesAmount)
            {
                foreach (var resource in allResources)
                {
                    if(resource.IsCollectorAppointed == false)
                    {
                        resources.Add(resource);
                    }
                }
            }

            resources.Clear();
            allResources.Clear();
        }
    }
}
