using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    public void spawnRocket() {
        var theNewRocket = Instantiate(_rocketPrefab);
        theNewRocket.transform.position = _spawnPoint.position;
    }


    [SerializeField]
    private Transform _spawnPoint = null;

    [SerializeField]
    private RocketMovement _rocketPrefab = null;
}
