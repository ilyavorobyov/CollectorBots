using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Collector> _collectors;

    private void Awake()
    {
        if(_collectors != null)
        {
            foreach(Collector collector in _collectors)
            {
                collector.Init(transform.position);
            }
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
}