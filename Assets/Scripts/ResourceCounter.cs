using TMPro;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _resorceCounterText;

    private int _resourcesNumber;

    private void Awake()
    {
        _resourcesNumber = 0;
    }

    private void OnEnable()
    {
        BaseCollisionHandler.ResourceCollected += AddResource;
    }

    private void OnDisable()
    {
        BaseCollisionHandler.ResourceCollected -= AddResource;
    }

    private void AddResource()
    {
        _resourcesNumber++;
        _resorceCounterText.text = _resourcesNumber.ToString();
    }
}