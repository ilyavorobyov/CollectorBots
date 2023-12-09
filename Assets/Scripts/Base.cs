using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Collector _collector;

    private List<Collector> _collectors = new List<Collector>();
    private int _startCollectorsNumber = 3;

    private void Start()
    {
        for(int i = 0;  i < _startCollectorsNumber; i++)
        {
            AddNewCollector();
        }
    }

    public void SearchFreeCollector(Resource resource)
    {
        foreach (var collector in _collectors)
        {
            if(!collector.IsBusyCollecting) 
            {
                collector.AppointResource(resource);
                return;
            }
        }
    }

    public void AddNewCollector()
    {
        Collector collector = Instantiate(_collector, transform.position, Quaternion.identity);
        collector.Init(transform.position);
        _collectors.Add(collector);
    }
}