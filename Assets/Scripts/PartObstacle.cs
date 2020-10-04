using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartObstacle : MonoBehaviour
{
    public bool IsCanBreak = false;
    public Obstacle parent = null;
    public MeshRenderer meshRenderer = null;
    public RocketTarget rocketTarget = null;
    public ImpactReactionTrigger impactReactionTrigger = null;

    // Start is called before the first frame update
    void Start() {
        rocketTarget.onHittedByRocket = (RocketMovement inRocket)=>performDestroy();
        impactReactionTrigger.onImpacted = (ImpactReactionTrigger inOtherTrigger)=>performDestroy();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            performDestroy();
        }
    }

    private void performDestroy() {
        if (IsCanBreak) {
            parent.CurrenNumberOfDestructibleBlocks--;
            gameObject.SetActive(false);
        }
    }
}
