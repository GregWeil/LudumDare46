using UnityEngine;

public class CannonController : MonoBehaviour {
    private GameObject Keep;

    void Start() {
        Keep = GameObject.Find("Keep");
    }

    void Update() {
        transform.position = Keep.transform.position;
    }

    void OnTower() {
        SendMessageUpwards("ExitKeep");
    }
}
