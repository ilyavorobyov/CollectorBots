using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Base))]
public class BaseWallet : MonoBehaviour
{
    private Base _base;
    private int _resourcesNumber;
    private int _collectorPrice = 3;

    public static event UnityAction<int> ResourceCollected;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    public void AddResource()
    {
        _resourcesNumber++;

        if(_resourcesNumber % _collectorPrice == 0)
        {
            _resourcesNumber -= _collectorPrice;
            _base.AddNewCollector();
        }

        ResourceCollected?.Invoke(_resourcesNumber);
    }
}