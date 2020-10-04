using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField] private NewIterationController iterationController;

    private void Start() {
        iterationController = (NewIterationController)FindObjectOfType(typeof(NewIterationController));
    }

    public void spawnRocket(SpaceShipMovement inShooterSpaceShipMovement) {
        var theNewRocket = Instantiate(_rocketPrefab);
        theNewRocket.transform.position = _spawnPoint.position;
        theNewRocket.setShooterSpaceShip(inShooterSpaceShipMovement);
        //iterationController.activeBulletsOnScene.Add(theNewRocket.gameObject);
    }


    [SerializeField]
    private Transform _spawnPoint = null;

    [SerializeField]
    private RocketMovement _rocketPrefab = null;
}
