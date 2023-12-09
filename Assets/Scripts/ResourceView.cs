using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private TMP_Text _resorceCounterText;

    private void OnEnable()
    {
        BaseWallet.ResourceCollected += OnShowResourcesNumber;
    }

    private void OnDisable()
    {
        BaseWallet.ResourceCollected -= OnShowResourcesNumber;
    }

    private void OnShowResourcesNumber(int resourcesNumber)
    {
        _resorceCounterText.text = resourcesNumber.ToString();
    }
}