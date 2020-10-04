using UnityEngine;

public class ImpactReactionTrigger : MonoBehaviour
{
    public System.Action<ImpactReactionTrigger> onImpacted;

    private void OnTriggerEnter(Collider other) {
        var theOtherTrigget = other.GetComponent<ImpactReactionTrigger>();
        if (theOtherTrigget)
            onImpacted?.Invoke(theOtherTrigget);
    }
}
