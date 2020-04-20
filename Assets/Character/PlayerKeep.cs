using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeep : MonoBehaviour {
    private GameObject Keep;
    
    public GameObject WaypointPrefab;
    public float EnterRange;

    void Start() {
        Keep = GameObject.Find("Keep");
    }

    void Update() {
        var offset = Keep.transform.position - transform.position;
        offset.y = 0;
        if (offset.magnitude < EnterRange) {
            SendMessageUpwards("EnterKeep", Keep);
        }
    }

    void OnTower(InputValue value) {
        if (value.isPressed && isActiveAndEnabled) {
            Keep.GetComponent<KeepMovement>().MarkPosition(transform.position);
            var waypoint = Instantiate(WaypointPrefab);
            waypoint.name = WaypointPrefab.name;
            waypoint.transform.position = transform.position + Vector3.back * 0.5f;
        }
    }

    void Dead() {
        enabled = false;
    }
}
