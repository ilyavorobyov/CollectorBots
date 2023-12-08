using UnityEngine;

public class Resource : MonoBehaviour
{
    private bool _isCollectorAppointed = false;
    private bool _isCollected = false;

    public void AppointCollector()
    {
        _isCollectorAppointed = true;
    }

    public bool CheckCanCollected()
    {
        return !_isCollectorAppointed && !_isCollected;
    }

    public void MakeCollected()
    {
        _isCollected = true;
    }
}