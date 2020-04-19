using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepController : MonoBehaviour {
    public int Health;
    public int Ammo;

    void Damage() {
        Health -= 1;
    }
}
