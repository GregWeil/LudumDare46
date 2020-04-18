using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeep : MonoBehaviour {
    void OnTower(InputValue value) {
        if (value.isPressed) {
            var tower = GameObject.FindObjectOfType<KeepMovement>();
            if (tower != null) tower.MarkPosition(transform.position);
        }
    }
}
