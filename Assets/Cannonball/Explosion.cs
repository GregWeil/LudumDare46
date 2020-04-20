using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    [HideInInspector]
    public GameObject IgnoreObject;

    void Start() {
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle() {
        yield return new WaitForSeconds(0.05f);
        
        var sphere = GetComponent<SphereCollider>();
        var hitObjects = new HashSet<Transform>();
        var collisions = Physics.OverlapSphere(transform.position, sphere.radius);
        foreach (var collider in collisions) {
            if (hitObjects.Contains(collider.transform.root)) continue;
            if (IgnoreObject != null && collider.transform.IsChildOf(IgnoreObject.transform)) continue;
            collider.SendMessageUpwards("Damage", SendMessageOptions.DontRequireReceiver);
            hitObjects.Add(collider.transform.root);
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
