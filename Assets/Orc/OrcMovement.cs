using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMovement : MonoBehaviour {
    private Animator Animation;

    public float MoveSpeed;

    void Start() {
        Animation = GetComponentInChildren<Animator>();
    }

    void Update() {
        
    }
}
