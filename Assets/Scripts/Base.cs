using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWallet))]
[RequireComponent(typeof(ResourceScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private Collector _collector;

    private List<Collector> _collectors = new List<Collector>();
    private BaseWallet _baseWallet;
    private ResourceScanner _resourceScanner;
    private Flag _currentFlag;
    private int _startCollectorsNumber = 3;
    private float _checkIntervalForNewBaseResources = 0.1f;
    private Coroutine _collectionForNewBase;

    private void Awake()
    {
        _baseWallet = GetComponent<BaseWallet>();
        _resourceScanner = GetComponent<ResourceScanner>();

        for (int i = 0; i < _startCollectorsNumber; i++)
        {
            CreateNewCollector();
        }
    }

    public void FinishBuildNewBase()
    {
        _resourceScanner.enabled = true;
        _baseWallet.ReduceForNewBase();
        Destroy(_currentFlag.gameObject);
    }

    public void SearchFreeCollector(Resource resource)
    {
        Collector collector = TryGetCollector();

        if (collector != null)
        {
            collector.AppointResource(resource);
            return;
        }
    }

    public void CreateNewCollector()
    {
        Collector collector = Instantiate(_collector, transform.position, Quaternion.identity);
        collector.Init(this);
        _collectors.Add(collector);
    }

    public void SendCreateNewBatabase(Flag currentFlag)
    {
        _currentFlag = currentFlag;
        _baseWallet.SaveForNewBase();

        if (_collectionForNewBase != null)
            StopCoroutine(_collectionForNewBase);

        _collectionForNewBase = StartCoroutine(CollectionForNewBase());
    }

    public void AddNewCollector(Collector collector)
    {
        _collectors.Add(collector);
    }

    private Collector TryGetCollector()
    {
        foreach (var collector in _collectors)
        {
            if (!collector.IsBusyCollecting)
            {
                return collector;
            }
        }

        return null;
    }

    private IEnumerator CollectionForNewBase()
    {
        bool isCheckResources = true;
        var waitForSeconds = new WaitForSeconds(_checkIntervalForNewBaseResources);
        Collector collector = TryGetCollector();

        while (isCheckResources)
        {
            if (_baseWallet.CheckEnoughForNewBase() && collector != null)
            {
                collector.InitBuildBase(_currentFlag.transform.position);
                _resourceScanner.enabled = false;
                isCheckResources = false;
                StopCoroutine(_collectionForNewBase);
            }
            else
            {
                collector = TryGetCollector();
            }

            yield return waitForSeconds;
        }
    }
}