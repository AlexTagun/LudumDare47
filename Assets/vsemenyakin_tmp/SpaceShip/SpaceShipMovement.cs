using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    public void makeHackStop() {
        _frontSpeedUnitsPerSecond = 0f;
    }

    public bool isInverted() {
        return (_frontSpeedUnitsPerSecond < 0);
    }

    public void moveRight() {
        changeTargetSidePosition(new Vector2Int(isInverted() ? -1 : 1, 0));
    }

    public void moveLeft() {
        changeTargetSidePosition(new Vector2Int(isInverted() ? 1 : -1, 0));
    }

    public void moveUp() {
        changeTargetSidePosition(new Vector2Int(0, 1));
    }

    public void moveDown() {
        changeTargetSidePosition(new Vector2Int(0, -1));
    }

    private void Start() {
        setupInitialStepRelatedState();
        SetStartPlayerSpeed();
    }

    private void setupInitialStepRelatedState() {
        _initialSidePosition = transform.position;
    }

    private void changeTargetSidePosition(Vector2Int inDeltaSideStep) {
        _targetSideStepPosition.x = Mathf.Clamp(_targetSideStepPosition.x + inDeltaSideStep.x,
            _limits.leftLimits, _limits.rightLimits);
        _targetSideStepPosition.y = Mathf.Clamp(_targetSideStepPosition.y + inDeltaSideStep.y,
            _limits.downLimits, _limits.upLimits);

        Vector2 theDeltaSideVector = targetSidePosition - currentSidePosition;
        float theSideStepDistance = theDeltaSideVector.magnitude / step;

        _timeToAchieveTargetPosition = theSideStepDistance / _sideStepsPerSecondVelocity;
    }

    private void FixedUpdate() {
        /*if (isPlayer)
            updateScaleFrontMovement();
        else
            updateFrontMovement();*/
        updateScaleFrontMovement();
        updateSideMovement();
        updateRotationFromSpeed();
    }

    private void updateFrontMovement() {
        float theFrontSpeedUnitsPerFrame = _frontSpeedUnitsPerSecond * Time.fixedDeltaTime;
        transform.position += frontDirection * theFrontSpeedUnitsPerFrame;
    }

    private void updateScaleFrontMovement()
    {
        if (timeUpSpeed >= ConfigManager.Data.IntervalBetweenSpeedIncrease)
        {
            _frontSpeedUnitsPerSecond += (isPlayer)? ConfigManager.Data.PlusToSpeed : -ConfigManager.Data.PlusToSpeed;
            timeUpSpeed = 0f;
        }
        else
        {
            timeUpSpeed += Time.fixedDeltaTime;
        }
        float theFrontSpeedUnitsPerFrame = _frontSpeedUnitsPerSecond * Time.fixedDeltaTime;
        transform.position += frontDirection * theFrontSpeedUnitsPerFrame;
    }

    private void updateSideMovement() {
        Vector3 theDeltaSideVector3 = computeFrameSideVelocity();
        transform.position += theDeltaSideVector3;

        _timeToAchieveTargetPosition -= Time.fixedDeltaTime;
        _timeToAchieveTargetPosition = Mathf.Max(_timeToAchieveTargetPosition, 0f);
    }

    private void updateRotationFromSpeed() {
        Vector2 theSideVelocity = computeFrameSideVelocity();

        float theHackFrontVelocityRotationCorrection = isInverted() ? 180f : 0f;

        float theDegreesPerSpeed = 50f;
        _visualTransform.transform.rotation = Quaternion.Euler(
            -theSideVelocity.y * theDegreesPerSpeed,
            theSideVelocity.x * theDegreesPerSpeed * (isInverted() ? -1f : 1f) + theHackFrontVelocityRotationCorrection,
            0f);
    }

    private Vector2 computeFrameSideVelocity() {
        Vector2 theDeltaSideVector = targetSidePosition - currentSidePosition;
        float theDeltaSideVectorMagnitude = theDeltaSideVector.magnitude;
        if (Mathf.Abs(theDeltaSideVectorMagnitude) < 0.1f || 0f == _timeToAchieveTargetPosition)
            return Vector2.zero;

        float theFrameSideSpeed = theDeltaSideVectorMagnitude / _timeToAchieveTargetPosition;
        float theFrameSideDistnace = theFrameSideSpeed * Time.fixedDeltaTime;

        if (theDeltaSideVectorMagnitude > theFrameSideDistnace) {
            return theDeltaSideVector / theDeltaSideVectorMagnitude * theFrameSideDistnace;
        } else {
            return Vector2.zero;
        }
    }

    public void SetStartPlayerSpeed()
    {
        _frontSpeedUnitsPerSecond = (isPlayer)? ConfigManager.Data.StartShipSpeed : -ConfigManager.Data.StartShipSpeed;
    }
    private float step => _step;
    private Vector3 frontDirection => Vector3.forward;

    private Vector2 currentSidePosition => transform.position;
    private Vector2 targetSidePosition => _initialSidePosition + new Vector2(_targetSideStepPosition.x * _step, _targetSideStepPosition.y * _step);

    //Fields
    [SerializeField]
    private float _step = 10f;

    [SerializeField]
    private float _frontSpeedUnitsPerSecond = 1f;

    [SerializeField]
    private float _sideStepsPerSecondVelocity = 1f;

    [System.Serializable]
    struct MovementStepLimits {
        public int leftLimits;
        public int rightLimits;
        public int downLimits;
        public int upLimits;
    };

    [SerializeField]
    private MovementStepLimits _limits;

    [SerializeField]
    private Transform _visualTransform = null;

    private Vector2 _initialSidePosition = Vector2.zero;
    private Vector2Int _targetSideStepPosition = Vector2Int.zero;
    private float _timeToAchieveTargetPosition = 0f;

    public bool isPlayer = false;
    private float timeUpSpeed = 0f;
    public void TPlayer()
    {
        _initialSidePosition = transform.position;
        _targetSideStepPosition = Vector2Int.zero;
        _timeToAchieveTargetPosition = 0f;
    }
}
