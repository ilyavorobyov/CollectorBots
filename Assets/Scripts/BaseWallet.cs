using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Base))]
public class BaseWallet : MonoBehaviour
{
    private Base _base;
    private int _resourcesNumber;
    private int _collectorPrice = 3;
    private int _basePrice = 5;
    private bool _isSavingForNewCollector = true;

    public static event UnityAction<int> ResourceCollected;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    public void AddResource()
    {
        _resourcesNumber++;

        if (_isSavingForNewCollector)
        {
            if (_resourcesNumber % _collectorPrice == 0)
            {
                _resourcesNumber -= _collectorPrice;
                _base.CreateNewCollector();
            }
        }

        ResourceCollected?.Invoke(_resourcesNumber);
    }

    public void SaveForNewBase()
    {
        _isSavingForNewCollector = false;
    }

    public bool CheckEnoughForNewBase()
    {
        return _resourcesNumber >= _basePrice;
    }

    public void ReduceForNewBase()
    {
        _resourcesNumber -= _basePrice;
        _isSavingForNewCollector = true;
        ResourceCollected?.Invoke(_resourcesNumber);
    }
}