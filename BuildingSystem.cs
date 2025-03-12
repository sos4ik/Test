using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private Structure[] _structureData;
    [SerializeField] private GameObject _outline;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _buildingLayer;

    [SerializeField] private Color _greenColor = Color.green;
    [SerializeField] private Color _redColor = Color.red;

    private IBuildable _currentBuilding;
    private IOutline _currentOutline;

    private PlacementValidator _placementValidator;

    private void Awake()
    {
        _placementValidator = new PlacementValidator(_buildingLayer);
    }

    private void Update()
    {
        if (_currentBuilding != null)
        {
            MoveBuildingWithCursor();
            CheckPlacement();

            if (Input.GetMouseButtonDown(0))
                TryPlaceBuilding();
        }
    }

    private void MoveBuildingWithCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            _currentBuilding.Build.transform.position = hit.point;
            _currentOutline?.UpdatePosition(hit.point);
        }
    }

    private void CheckPlacement()
    {
        bool canPlace = _placementValidator.CanPlace(_currentBuilding.Build.transform.position, _currentBuilding.Size);
        _currentOutline?.SetColor(canPlace ? _greenColor : _redColor);
    }

    private void TryPlaceBuilding()
    {
        bool canPlace = _placementValidator.CanPlace(_currentBuilding.Build.transform.position, _currentBuilding.Size);
        if (canPlace)
        {
            _currentBuilding.Place();
            _currentOutline?.Destroy();
            _currentBuilding = null;
            _currentOutline = null;
        }
    }

    public void SelectBuilding(int index)
    {
        if (_currentBuilding != null)
        {
            Destroy(_currentBuilding.Build);
            _currentOutline?.Destroy();
        }

        Structure selectedStrucure = _structureData[index];

        GameObject structureInstance = Instantiate(selectedStrucure.Prefab);

        _currentBuilding = structureInstance.GetComponent<IBuildable>();

        if (_outline != null)
        {
            GameObject outlineInstance = Instantiate(_outline);
            _currentOutline = outlineInstance.GetComponent<IOutline>();
            _currentOutline.UpdatePosition(_currentBuilding.Build.transform.position);
        }
    }
}
