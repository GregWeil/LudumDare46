using UnityEngine;
using UnityEngine.UI;

public class KeepController : MonoBehaviour {
    public Text HealthDisplay;
    public Text AmmoDisplay;

    public int Health;
    public int Ammo;

    public float DamageTimeout;
    private float LastDamage = 0f;

    void Update() {
        HealthDisplay.text = $"{Health}";
        AmmoDisplay.text = $"{Ammo}";
    }

    void Damage() {
        if (Time.time >= LastDamage + DamageTimeout) {
            Health -= 1;
            LastDamage = Time.time;
        }
    }
}
