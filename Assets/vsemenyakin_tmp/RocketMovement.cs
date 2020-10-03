using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    private void FixedUpdate() {
        updateCollisions();
        updateMovement();
    }

    RaycastHit[] __hits = new RaycastHit[10];
    private void updateCollisions() {
        Ray theRay = new Ray(transform.position, frameVelocityVector.normalized);
        int theActualHitsNum = Physics.SphereCastNonAlloc(theRay, _collider.radius, __hits, frameVelocityVector.magnitude);

        bool theAnyCollisionFound = false;
        for (int theHitIndex = 0; theHitIndex < theActualHitsNum; ++theHitIndex) {
            ref RaycastHit theHit = ref __hits[theHitIndex];

            if (theHit.collider != _collider) {
                var theRocketTarget = theHit.collider.GetComponent<RocketTarget>();
                if (theRocketTarget) {
                    theRocketTarget?.onHittedByRocket?.Invoke(this);
                    theAnyCollisionFound = true;

                    Debug.Log(theHit.collider);
                }
            }

            theHit = new RaycastHit();
        }

        if (theAnyCollisionFound) {
            Destroy(gameObject);
        }
    }

    private void updateMovement() {
        transform.position += frameVelocityVector;
    }

    private Vector3 velocityVector => transform.forward * _speed;
    private Vector3 frameVelocityVector => velocityVector * Time.fixedDeltaTime;

    [SerializeField]
    private float _speed = 15f;

    [SerializeField]
    private SphereCollider _collider = null;

}
