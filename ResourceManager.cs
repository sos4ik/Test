using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance; 

    [SerializeField] private Text _resourcesText; 

    private int _totalResources = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddResources(int amount)
    {
        _totalResources += amount;
        UpdateUI();
    }

    public void SubtractResources(int amount)
    {
        _totalResources -= amount;
        UpdateUI();
    }

    public int GetResources()
    {
        return _totalResources;
    }

    private void UpdateUI()
    {
        if (_resourcesText != null)
        {
            _resourcesText.text = " " + _totalResources;
        }
    }
}
