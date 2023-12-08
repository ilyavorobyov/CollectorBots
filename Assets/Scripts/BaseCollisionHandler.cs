using UnityEngine;
using UnityEngine.Events;

public class BaseCollisionHandler : MonoBehaviour
{
    public static event UnityAction ResourceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Resource resource))
        {
            ResourceCollected?.Invoke();
            resource.MakeCollected();
        }
    }
}