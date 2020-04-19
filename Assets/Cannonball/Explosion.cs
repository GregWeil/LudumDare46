using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour {
    void Start() {
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle() {
        var sphere = GetComponent<SphereCollider>();
        var collisions = Physics.OverlapSphere(transform.position, sphere.radius);
        foreach (var collider in collisions) {
            collider.SendMessageUpwards("Damage", SendMessageOptions.DontRequireReceiver);
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
