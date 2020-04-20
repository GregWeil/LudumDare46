using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeep : MonoBehaviour {
    private GameObject Keep;
    private bool BlockEntry = false;
    
    public GameObject WaypointPrefab;
    public float EnterRange;

    public class TryEnterKeepEvent {
        public GameObject Keep;
        public bool Cancel = false;
    }

    void Start() {
        Keep = GameObject.Find("Keep");
    }

    void Update() {
        var offset = Keep.transform.position - transform.position;
        offset.y = 0;
        if (offset.magnitude > EnterRange) {
            BlockEntry = false;
        } else if (!BlockEntry) {
            BeginTryEnterKeep();
        }
    }

    void BeginTryEnterKeep() {
        var enterEvent = new TryEnterKeepEvent { Keep = Keep };
        SendMessage("TryEnterKeep", enterEvent);
        BlockEntry = enterEvent.Cancel;
        if (!BlockEntry) SendMessageUpwards("EnterKeep", Keep);
    }

    void OnTower(InputValue value) {
        if (value.isPressed && isActiveAndEnabled) {
            var offset = Keep.transform.position - transform.position;
            offset.y = 0;
            if (offset.magnitude > EnterRange) {
                Keep.GetComponent<KeepMovement>().MarkPosition(transform.position);
                var waypoint = Instantiate(WaypointPrefab);
                waypoint.name = WaypointPrefab.name;
                waypoint.transform.position = transform.position + Vector3.back * 0.5f;
            } else {
                BeginTryEnterKeep();
            }
        }
    }

    void Dead() {
        enabled = false;
    }
}
