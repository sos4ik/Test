using UnityEngine;

[CreateAssetMenu(fileName = "ArrowMan", menuName = "UnitData/ArrowMan", order = 51)]
public class ArrowMan : UserData
{
    [SerializeField] private float _minDistanceAttack;
    [SerializeField] private float _maxDistanceAttack;

    [SerializeField] private int _attackPerSecond;

    [SerializeField] private int _damage;

    public float MaxDistanceAttack => _maxDistanceAttack;
}
