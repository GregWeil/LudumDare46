using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public GameObject SoundPrefab;

    public float DropSpeed;
    public int Health;
    public int Ammo;

    [HideInInspector]
    public bool Held;

    public void Apply(KeepController keep) {
        var initialHealth = Health;
        var initialAmmo = Ammo;

        if (!keep.Dead) {
            keep.Health += Health;
            if (keep.Health > keep.MaxHealth) {
                Health = keep.Health - keep.MaxHealth;
                keep.Health = keep.MaxHealth;
            } else {
                Health = 0;
            }
            
            keep.Ammo += Ammo;
            Ammo = 0;
        }

        if (Health < initialHealth || Ammo < initialAmmo) {
            var sound = Instantiate(SoundPrefab, transform.position, transform.rotation);
            sound.name = SoundPrefab.name;
            Destroy(sound, 1f);
        }

        if (Health <= 0 && Ammo <= 0) {
            Destroy(gameObject);
        }
    }

    IEnumerator DropCycle() {
        var targetPosition = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
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
