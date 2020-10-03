using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTarget : MonoBehaviour
{
    public System.Action<RocketMovement> onHittedByRocket = null;
}
