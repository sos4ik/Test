using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private int _totalResources = 100; 
    [SerializeField] private int _resourcesPerSecond = 10; 

    private bool _isBeingHarvested = false; 
    private int _currentResources; 

    private void Start()
    {
        _currentResources = _totalResources; 
    }

    public void StartHarvesting()
    {
        if (!_isBeingHarvested)
        {
            _isBeingHarvested = true;
            InvokeRepeating(nameof(HarvestResource), 1f, 1f);
        }
    }

    public void StopHarvesting()
    {
        if (_isBeingHarvested)
        {
            _isBeingHarvested = false;
            CancelInvoke(nameof(HarvestResource)); 
        }
    }

    private void HarvestResource()
    {
        if (_currentResources > 0)
        {
            int harvestedAmount = Mathf.Min(_resourcesPerSecond, _currentResources);
            _currentResources -= harvestedAmount;

            ResourceManager.Instance.AddResources(harvestedAmount);

            if (_currentResources <= 0)
            {
                DestroyResource();
            }
        }
    }

    private void DestroyResource()
    {
        StopHarvesting();
        Destroy(gameObject); 
    }
}