using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public float DropSpeed;
    public int Health;
    public int Ammo;

    [HideInInspector]
    public bool Held;

    public void Apply(KeepController keep) {
        keep.Health += Health;
        keep.Ammo += Ammo;
        Destroy(gameObject);
    }

    IEnumerator DropCycle() {
        var targetPosition = Vector3.Scale(transform.TransformPoint(0, 0, 1), new Vector3(1, 0, 1));
        while (transform.position.y > 0f) {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, DropSpeed * Time.deltaTime);
        }
        Held = false;
    }

    void Drop() {
        StartCoroutine(DropCycle());
    }
}
