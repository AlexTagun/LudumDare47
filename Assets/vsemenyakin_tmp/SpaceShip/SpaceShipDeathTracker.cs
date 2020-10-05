using UnityEngine;

public class SpaceShipDeathTracker : MonoBehaviour
{
    private void Awake() {
        _impactReactionTrigger.onImpacted = (ImpactReactionTrigger inImpactReactionTrigger) => {
            var theSpaceshipMovement = _rootSpaceShipGameObjectToDestroyOnDeath.GetComponent<SpaceShipMovement>();

            if (isPlayer) {
                spaceShipManager.processPlayerSpaceShipDeath(theSpaceshipMovement);
            } else {
                bool theIsPlayerImpacted = inImpactReactionTrigger.GetComponentInParent<SpaceShipPlayerController>();
                spaceShipManager.processCloneSpaceShipDeath(theSpaceshipMovement, theIsPlayerImpacted);
            }
        };

        _rocketTarget.onHittedByRocket = (RocketMovement inRocketMovement) => {
            var theSpaceshipMovement = _rootSpaceShipGameObjectToDestroyOnDeath.GetComponent<SpaceShipMovement>();

            if (isPlayer)
            {
                if (!inRocketMovement.isShootedByPlayer)
                    spaceShipManager.processPlayerSpaceShipDeath(theSpaceshipMovement);
            }
            else
            {
                bool theIsPlayerImpacted = inRocketMovement ? inRocketMovement.isShootedByPlayer : false;
                spaceShipManager.processCloneSpaceShipDeath(theSpaceshipMovement, theIsPlayerImpacted);
            }
        };
    }

    private SpaceShipManager spaceShipManager => FindObjectOfType<SpaceShipManager>();


    [SerializeField]
    private bool isPlayer = true;

    [SerializeField]
    private ImpactReactionTrigger _impactReactionTrigger;

    [SerializeField]
    private RocketTarget _rocketTarget;

    [SerializeField]
    private GameObject _rootSpaceShipGameObjectToDestroyOnDeath;
}
