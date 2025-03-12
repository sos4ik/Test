using UnityEngine;

public class UnitHarvester : MonoBehaviour
{
    private Resource _currentResource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Resource"))
        {
            _currentResource = other.GetComponent<Resource>();
            if (_currentResource != null)
                _currentResource.StartHarvesting();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Resource"))
        {
            if(_currentResource != null)
            {
                _currentResource.StopHarvesting();
                _currentResource = null;
            }
        }
    }
}
