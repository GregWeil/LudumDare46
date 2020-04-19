using UnityEngine;

public static class Parabola {
    public static Vector3 PositionAtTime(Vector3 startPosition, Vector3 endPosition, float startTime, float duration, float height, float time) {
        var proportion = (time - startTime) / duration;
        var position = Vector3.LerpUnclamped(startPosition, endPosition, proportion);
        position.y += height * (1f - Mathf.Pow(proportion * 2f - 1f, 2f));
        return position;
    }
}
