﻿using System.Linq;
using UnityEngine;

public class Cannonball : MonoBehaviour {
    private Vector3 StartPosition;
    private float StartTime;
    public GameObject ExplosionPrefab;
    [HideInInspector]
    public GameObject IgnoreObject;
    [HideInInspector]
    public Vector3 TargetPosition;
    public float Height;
    public float Duration;

    void Start() {
        StartPosition = transform.position;
        StartTime = Time.time;
    }

    void Update() {
        transform.position = Parabola.PositionAtTime(StartPosition, TargetPosition, StartTime, Duration, Height, Time.time);
        var collisions = Physics.OverlapSphere(transform.position, 0.5f);
        if (collisions.Any(collider => !collider.transform.IsChildOf(IgnoreObject.transform) && !collider.transform.IsChildOf(transform))) {
            Explode();
        } else if (transform.position.y < -100f) {
            Destroy(gameObject);
        }
    }

    void Explode() {
        var explosion = Instantiate(ExplosionPrefab);
        explosion.name = ExplosionPrefab.name;
        explosion.transform.position = transform.position;
        explosion.GetComponent<Explosion>().IgnoreObject = IgnoreObject;
        Destroy(gameObject);
    }

    void Damage() {
        Explode();
    }
}
