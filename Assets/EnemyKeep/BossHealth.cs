using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {
    private Vector3 ShakeOffset = Vector3.zero;
    private float ShakeTimer = 0f;

    public EnemyCannon[] Cannons;

    public int Health;
    public float FallSpeed;
    public float ShakeInterval;
    public float ShakeRadius;

    void Update() {
        if (Health <= 0) {
            foreach (var cannon in Cannons) {
                cannon.CooldownTime = float.PositiveInfinity;
            }

            if (ShakeTimer < 0f) {
                transform.Translate(-ShakeOffset);
                ShakeOffset = Random.insideUnitSphere * ShakeRadius;
                transform.Translate(ShakeOffset);
                ShakeTimer = ShakeInterval;
            }
            ShakeTimer -= Time.deltaTime;

            transform.Translate(0, -FallSpeed * Time.deltaTime, 0);
            if (transform.position.y < -15f) Destroy(gameObject);
        }
    }

    void Damage() {
        Health -= 1;
    }
}
