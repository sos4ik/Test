using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab; 

    [SerializeField] private float _spawnRadius = 5f; 

    [SerializeField] private int _spawnCost; 

    [SerializeField] private LayerMask _obstacleLayer; 

    public void TrySpawnUnit()
    {
        if (ResourceManager.Instance.GetResources() >= _spawnCost)
        {
            Vector3 spawnPosition = FindSpawnPosition();
            if (spawnPosition != Vector3.zero) 
            {
                Instantiate(_unitPrefab, spawnPosition, Quaternion.identity);

                ResourceManager.Instance.SubtractResources(_spawnCost);
            }
            else
            {
                Debug.Log("Нет подходящего места для спавна!");
            }
        }
        else
        {
            Debug.Log("Недостаточно ресурсов для спавна юнита!");
        }
    }
    private Vector3 FindSpawnPosition()
    {
        for (int i = 0; i < 10; i++) 
        {
            Vector2 randomCircle = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);

            if (!Physics.CheckSphere(spawnPosition, 1f, _obstacleLayer))
            {
                return spawnPosition; 
            }
        }

        return Vector3.zero; 
    }
}
