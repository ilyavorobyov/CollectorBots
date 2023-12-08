using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsCollectorAppointed { get; private set; }

    private void Awake()
    {
        IsCollectorAppointed = false;
    }

    public void AppointCollector()
    {
        IsCollectorAppointed = true;
    }
}