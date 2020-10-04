using UnityEngine;

public class SpaceShipDeathTracker : MonoBehaviour
{
    private void Awake() {
        _impactReactionTrigger.onImpacted = (ImpactReactionTrigger unused) => processDeath();
        _rocketTarget.onHittedByRocket = (RocketMovement unused) => processDeath();
    }

    private void processDeath() {
        //TODO: Before death actions:
        //1. If player dead - register it's replay
        //2. If clone dead - check dead cause and remove clone replay if dead because of player rocket

        Destroy(_rootSpaceShipGameObjectToDestroyOnDeath);
    }

    [SerializeField]
    private bool isPlayer = true;

    [SerializeField]
    private ImpactReactionTrigger _impactReactionTrigger;

    [SerializeField]
    private RocketTarget _rocketTarget;

    [SerializeField]
    private GameObject _rootSpaceShipGameObjectToDestroyOnDeath;
}
