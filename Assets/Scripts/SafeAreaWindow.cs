using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaWindow : SafeArea {
    protected override void ApplySafeArea(Rect r) {
        Vector2 anchorMin = r.position;
        Vector2 anchorMax = r.position + r.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        Panel.anchorMax = anchorMax;
        Panel.anchorMin = anchorMin;;
    }
}