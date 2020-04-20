using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMovement : MonoBehaviour {
    private SpriteRenderer Sprite;
    private Animator Animation;
    private Vector3 Target;
    private bool Aggro;
    private bool Attacking;

    public AudioSource AttackSound;

    public float MoveSpeed;
    public float AggroMoveSpeed;
    public float AggroRange;
    public float TelegraphTime;
    public float SwingTime;

    void Start() {
        Sprite = GetComponentInChildren<SpriteRenderer>();
        Animation = GetComponentInChildren<Animator>();
        Target = transform.position;
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));
            if (Attacking) continue;
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
        }
    }

    IEnumerator Attack() {
        Attacking = true;
        Animation.SetTrigger("Telegraph");
        yield return new WaitForSeconds(TelegraphTime);
        Animation.SetTrigger("Swing");
        AttackSound.Play();
        yield return new WaitForSeconds(SwingTime);
        Animation.SetTrigger("AttackDone");
        Attacking = false;
        Aggro = false;
    }

    void Update() {
        var speed = Aggro ? AggroMoveSpeed : MoveSpeed;
        if (Aggro && Vector3.Distance(transform.position, Target) < 2f) {
            if (!Attacking) StartCoroutine(Attack());
            speed = 0f;
        }
        var velocity = speed * (Target - transform.position).normalized;
        velocity.y = 0f;
        transform.position += velocity * Time.deltaTime;

        Animation.SetFloat("Speed", velocity.magnitude);
        if (Mathf.Abs(velocity.x) > 1f) {
            Sprite.flipX = velocity.x < 0f;
        }
    }
}
