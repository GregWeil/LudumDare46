using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeep : MonoBehaviour {
    private GameObject Keep;

    public float EnterRange;

    void Start() {
        Keep = GameObject.Find("Keep");
    }

    void Update() {
        var offset = Keep.transform.position - transform.position;
        offset.y = 0;
        if (offset.magnitude < EnterRange) {
            SendMessageUpwards("EnterKeep");
        }
    }

    void OnTower(InputValue value) {
        if (value.isPressed && isActiveAndEnabled) {
            Keep.GetComponent<KeepMovement>().MarkPosition(transform.position);
        }
    }

    void Dead() {
        enabled = false;
    }
}
