using System.Collections.Generic;

public class SpaceShipActionsReplay
{
    public enum ActionType {
        MoveRight,
        MoveLeft,
        MoveUp,
        MoveDown,
        SpawnRocket
    }

    public struct Action {
        public Action(float inTimeStamp, ActionType inType) {
            timeStamp = inTimeStamp;
            type = inType;
        }

        public float timeStamp;
        public ActionType type;
    }

    public struct Enumerator {
        internal Enumerator(List<Action>.Enumerator inNearestNextActionEnumerator) {
            _nearestNextActionEnumerator = inNearestNextActionEnumerator;
            _isCurrentValid = _nearestNextActionEnumerator.MoveNext();
        }

        public void moveProcessingPassedActions(float inNextTimeStamp, System.Action<Action> inProcessingDelegate) {
            while (_isCurrentValid && _nearestNextActionEnumerator.Current.timeStamp <= inNextTimeStamp) {
                inProcessingDelegate(_nearestNextActionEnumerator.Current);
                _isCurrentValid = _nearestNextActionEnumerator.MoveNext();
            }
        }

        private List<Action>.Enumerator _nearestNextActionEnumerator;
        private bool _isCurrentValid;
    }

    public void addAction(float inTimeStamp, ActionType inType) {
        _actions.Add(new Action(inTimeStamp, inType));
    }

    public Enumerator getEnumerator() {
        var theActionsEnumerator = _actions.GetEnumerator();
        return new Enumerator(theActionsEnumerator);
    }

    private List<Action> _actions = new List<Action>();
}
