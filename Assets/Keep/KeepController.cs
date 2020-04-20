using UnityEngine;
using UnityEngine.UI;

public class KeepController : MonoBehaviour {
    public Text HealthDisplay;
    public Text AmmoDisplay;

    public int Health;
    public int Ammo;

    void Update() {
        HealthDisplay.text = $"{Health}";
        AmmoDisplay.text = $"{Ammo}";
    }

    void Damage() {
        Health -= 1;
    }
}
