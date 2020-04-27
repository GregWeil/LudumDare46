using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public GameObject Target;
    public float MoveSpeed;
    public float PlayerWeight;

    void Update() {
        if (Target != null) {
            var targetPosition = Target.transform.position;
            var players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Any()) {
                var playersPosition = players.Aggregate(Vector3.zero, (position, player) => position + player.transform.position);
                playersPosition /= players.Length;
                targetPosition += (playersPosition - targetPosition) * PlayerWeight;
            }
            targetPosition.y = 0f;
            var lerp = 1f - Mathf.Pow(1f - MoveSpeed, Time.deltaTime*60f);
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerp);
        }
    }
}
