using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    public void moveRight() {
        changeTargetSidePosition(new Vector2Int(1, 0));
    }

    public void moveLeft() {
        changeTargetSidePosition(new Vector2Int(-1, 0));
    }

    public void moveUp() {
        changeTargetSidePosition(new Vector2Int(0, 1));
    }

    public void moveDown() {
        changeTargetSidePosition(new Vector2Int(0, -1));
    }

    private void Awake() {
        setupInitialState();
    }

    private void setupInitialState() {
        _targetSidePosition = transform.position;
    }

    private void changeTargetSidePosition(Vector2Int inDeltaSideStep) {
        Vector2 theDeltaSidePosition = new Vector2(
            inDeltaSideStep.x * step,
            inDeltaSideStep.y * step);
        _targetSidePosition += theDeltaSidePosition;

        Vector2 theDeltaSideVector = _targetSidePosition - currentSidePosition;
        float theSideStepDistance = theDeltaSideVector.magnitude / step;

        //_timeToAchieveTargetPosition = theSideStepDistance / _sideStepSpeed;
    }

    private void FixedUpdate() {
        updateSideMovement();
        updateFrontMovement();
    }
    private void updateSideMovement() {
        //_timeToAchieveTargetPosition -= Time.fixedDeltaTime;

        Vector2 theDeltaSideVector = _targetSidePosition - currentSidePosition;
        float theDeltaSideVectorMagnitude = theDeltaSideVector.magnitude;
        if (Mathf.Approximately(theDeltaSideVectorMagnitude, 0f))
            return;
        Vector2 theDeltaSideVectorNormal = theDeltaSideVector / theDeltaSideVectorMagnitude;

        float theSideVelocityMagnitude = _sideVelocityVector.magnitude;
        float theTimeToMakeSideVelocityZero = theSideVelocityMagnitude / _maxAcceleration;
        float theDistanceToMakeSideVelocityZero =
            (theSideVelocityMagnitude - _maxAcceleration * theTimeToMakeSideVelocityZero / 2f) *
            theTimeToMakeSideVelocityZero;
        bool theDoSpeedIncrease = (theDistanceToMakeSideVelocityZero > theDeltaSideVectorMagnitude);

        float theDeltaVelocity = _maxAcceleration * (theDoSpeedIncrease ? 1f : -1f);
        _sideVelocityVector += theDeltaSideVectorNormal * theDeltaVelocity;

        Vector3 theSideVelocity3 = _sideVelocityVector;
        transform.position += theSideVelocity3 * Time.deltaTime;

        //float theFrameSideSpeed = theDeltaSideVectorMagnitude / _timeToAchieveTargetPosition;
        //float theFrameSideDistnace = theFrameSideSpeed * Time.fixedDeltaTime;
        //
        //if (theDeltaSideVectorMagnitude > theFrameSideDistnace) {
        //    Vector3 theDeltaSideVector3 = theDeltaSideVector;
        //    transform.position += theDeltaSideVector3 / theDeltaSideVectorMagnitude * theFrameSideDistnace;
        //} else {
        //    transform.position = new Vector3(
        //        _targetSidePosition.x,
        //        _targetSidePosition.y,
        //        transform.position.z);
        //}
    }

    private void updateFrontMovement() {
        float theFrameSpeed = _frontSpeed * Time.fixedDeltaTime;
        transform.position += frontDirection * theFrameSpeed;
    }

    private float step => 5f;
    private Vector3 frontDirection => Vector3.forward;
    private Vector3 sideDirection => Vector3.right;

    private Vector2 currentSidePosition => transform.position;

    //Fields
    [SerializeField]
    float _frontSpeed = 1f;

    [SerializeField]
    float _timeToPassStep = 1f;

    [SerializeField]
    float _maxAcceleration = 0.1f;

    Vector2 _targetSidePosition = Vector2.zero;
    //float _timeToAchieveTargetPosition = 0f;
    Vector2 _sideVelocityVector = Vector2.zero;
}
