using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMovement : MonoBehaviour {
    private SpriteRenderer Sprite;
    private Animator Animation;
    private Vector3 Target;
    private bool Aggro;

    public float MoveSpeed;
    public float AggroMoveSpeed;
    public float AggroRange;

    void Start() {
        Sprite = GetComponentInChildren<SpriteRenderer>();
        Animation = GetComponentInChildren<Animator>();
        Target = transform.position;
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle() {
        while (true) {
            Aggro = false;
            var target = GameObject.Find("Keep");
            var players = GameObject.FindObjectsOfType<PlayerMovement>();
            foreach (var player in players) {
                var offset = player.transform.position - transform.position;
                offset.y = 0;
                if (offset.magnitude < AggroRange) {
                    Aggro = true;
                    target = player.gameObject;
                    break;
                }
            }
            Target = target.transform.position;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));
        }
    }

    void Update() {
        var speed = Aggro ? AggroMoveSpeed : MoveSpeed;
        var velocity = speed * (Target - transform.position).normalized;
        velocity.y = 0f;
        transform.position += velocity * Time.deltaTime;

        Animation.SetFloat("Speed", velocity.magnitude);
        if (Mathf.Abs(velocity.x) > 1f) {
            Sprite.flipX = velocity.x < 0f;
        }
    }
}
