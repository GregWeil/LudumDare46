﻿using UnityEngine;

public class BossHealth : MonoBehaviour {
    private Vector3 ShakeOffset = Vector3.zero;
    private float ShakeTimer = 0f;

    public EnemyCannon[] Cannons;

    public int Health;
    public float FallSpeed;
    public float ShakeInterval;
    public float ShakeRadius;

    void Update() {
        var shake = false;
        if (Health <= 0) {
            shake = true;
            foreach (var cannon in Cannons) {
                cannon.CooldownTime = float.PositiveInfinity;
            }
            transform.Translate(0, -FallSpeed * Time.deltaTime, 0);
            if (transform.position.y < -15f) Destroy(gameObject);
        } else if (transform.position.y < 0f) {
            shake = true;
            transform.Translate(0, FallSpeed * Time.deltaTime, 0);
            if (transform.position.y > 0f) {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
        if (!shake) {
            transform.Translate(-ShakeOffset);
            ShakeOffset = Vector3.zero;
        } else if (ShakeTimer < 0f) {
            transform.Translate(-ShakeOffset);
            ShakeOffset = Random.insideUnitSphere * ShakeRadius;
            transform.Translate(ShakeOffset);
            ShakeTimer = ShakeInterval;
        }
        ShakeTimer -= Time.deltaTime;
    }

    void Damage() {
        Health -= 1;
    }
}
