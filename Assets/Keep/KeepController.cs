using UnityEngine;
using UnityEngine.UI;

public class KeepController : MonoBehaviour {
    public Text HealthDisplay;
    public Text AmmoDisplay;

    public int MaxHealth;
    public int Health;
    public int Ammo;

    void Update() {
        HealthDisplay.text = $"{Health}/{MaxHealth}";
        AmmoDisplay.text = $"{Ammo}";
    }

    void Damage() {
        Health -= 1;
    }
}
