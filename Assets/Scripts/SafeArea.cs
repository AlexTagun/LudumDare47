using UnityEngine;

public abstract class SafeArea : MonoBehaviour {
    protected RectTransform Panel;
    private Rect LastSafeArea = new Rect(0, 0, 0, 0);

    private void Awake() {
        Panel = GetComponent<RectTransform>();
        Refresh();
    }

    private void Update() {
        Refresh();
    }

    private void Refresh() {
        Rect safeArea = GetSafeArea();

        if (safeArea != LastSafeArea) {
            LastSafeArea = safeArea;
            ApplySafeArea(safeArea);
            Debug.LogFormat("New safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}",
                name, safeArea.x, safeArea.y, safeArea.width, safeArea.height, Screen.width, Screen.height);
        }
    }

    private Rect GetSafeArea() {
        return Screen.safeArea;
    }

    protected abstract void ApplySafeArea(Rect r);
}