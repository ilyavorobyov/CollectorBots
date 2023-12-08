using TMPro;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _resorceCounterText;

    private int _resourcesNumber;

    private void Start()
    {
        _resorceCounterText.text = _resourcesNumber.ToString();
    }

    private void OnEnable()
    {
        BaseCollisionHandler.ResourceCollected += OnAddResource;
    }

    private void OnDisable()
    {
        BaseCollisionHandler.ResourceCollected -= OnAddResource;
    }

    private void OnAddResource()
    {
        _resourcesNumber++;
        _resorceCounterText.text = _resourcesNumber.ToString();
    }
}