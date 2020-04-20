using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeepController : MonoBehaviour {
    [HideInInspector]
    public bool Dead;

    public GameObject DeathParticles;
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
        if (Health < 0) {
            Health = 0;
            if (!Dead) {
                StartCoroutine(DeadCycle());
            }
            Dead = true;
        }
    }

    IEnumerator DeadCycle() {
        DeathParticles.SetActive(true);
        GetComponent<KeepMovement>().enabled = false;
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }
}
