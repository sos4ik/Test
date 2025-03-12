using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "UnitData/Unit", order = 51)]
public class UserData : ScriptableObject
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private string _unitName;

    [SerializeField] private int _unitHP;

    [SerializeField] private int _unitPrice;

    public GameObject Prefab => _prefab;

    public string UnitName => _unitName;

    public int UnitHP => _unitHP;

    public int UnitPrice => _unitPrice;
}
