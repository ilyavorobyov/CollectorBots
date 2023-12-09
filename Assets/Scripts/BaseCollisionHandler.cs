using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BaseWallet))]
public class BaseCollisionHandler : MonoBehaviour
{
    private BaseWallet _wallet;

    private void Awake()
    {
        _wallet = GetComponent<BaseWallet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Resource resource))
        {
            _wallet.AddResource();
            resource.MakeCollected();
        }
    }
}