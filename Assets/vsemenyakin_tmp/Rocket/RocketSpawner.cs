using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField] private NewIterationController iterationController;
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    

    private void Start() {
        iterationController = (NewIterationController)FindObjectOfType(typeof(NewIterationController));
    }

    public void spawnRocket(SpaceShipMovement inShooterSpaceShipMovement) {
        if (inShooterSpaceShipMovement.gameObject.name == "Spaceship") {
            if(_gameplayManager.CurBulletCount == 0) return;
            _gameplayManager.CurBulletCount--;
        }
        var theNewRocket = Instantiate(_rocketPrefab);
        _audioSource.PlayOneShot(_audioClip);
        if (isInverted())
            theNewRocket.makeInverted();
        Debug.Log($"Set speed - {inShooterSpaceShipMovement.FrontSpeedUnitsPerSecond}");
        theNewRocket.setSpeed(inShooterSpaceShipMovement.FrontSpeedUnitsPerSecond);
        theNewRocket.transform.position = _spawnPoint.position;
        theNewRocket.setShooterSpaceShip(inShooterSpaceShipMovement);
        iterationController.activeBulletsOnScene.Add(theNewRocket.gameObject);
    }

    private bool isInverted() {
        return (transform.rotation.eulerAngles.y != 0f);
    }

    [SerializeField]
    private Transform _spawnPoint = null;

    [SerializeField]
    private RocketMovement _rocketPrefab = null;
}
