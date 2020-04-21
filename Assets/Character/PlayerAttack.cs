using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour {
    private Animator Animation;
    private bool IsDead;
    private bool Attacking;

    void Start() {
        Animation = GetComponentInChildren<Animator>();
    }

    IEnumerator Attack() {
        var movement = GetComponent<PlayerMovement>();
        var speed = movement.MoveSpeed;
        movement.MoveSpeed *= 0.25f;
        Attacking = true;
        SendMessage("Drop");
        yield return new WaitForSeconds(0.15f);
        var hit = false;
        var colliders = Physics.OverlapSphere(transform.position, 2f, LayerMask.GetMask("Character"));
        foreach (var collider in colliders) {
            if (collider.tag == "Player") continue;
            collider.SendMessageUpwards("Damage");
            hit = true;
        }
        if (hit) GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.4f);
        if (!IsDead) movement.MoveSpeed = speed;
        Attacking = false;
    }

    void OnAttack(InputValue value) {
        if (value.isPressed && !IsDead && !Attacking) {
            Animation.SetTrigger("Attack");
            StartCoroutine(Attack());
        }
    }

    void Dead() {
        IsDead = true;
    }
}
