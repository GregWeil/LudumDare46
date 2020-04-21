using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMovement : MonoBehaviour {
    private SpriteRenderer Sprite;
    private Animator Animation;
    private Vector3 Target;
    private float TargetRadius;
    private bool Aggro;
    private bool Attacking;
    private bool AttackCooldown;

    public AudioSource AttackSound;

    public float MoveSpeed;
    public float AggroMoveSpeed;
    public float AggroRange;
    public float TelegraphTime;
    public float SwingTime;
    public float Cooldown;

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
            Target = GameObject.Find("Keep").transform.position;
            TargetRadius = 4f;
            var players = GameObject.FindObjectsOfType<PlayerMovement>();
            foreach (var player in players) {
                var offset = player.transform.position - transform.position;
                offset.y = 0;
                if (offset.magnitude < AggroRange) {
                    Aggro = true;
                    Target = player.transform.position;
                    TargetRadius = 2f;
                    break;
                }
            }
        }
    }

    IEnumerator Attack() {
        Attacking = true;
        Animation.SetTrigger("Telegraph");
        yield return new WaitForSeconds(TelegraphTime);
        Animation.SetTrigger("Swing");
        AttackSound.Play();
        if (enabled) {
            var handled = new HashSet<Transform>();
            var colliders = Physics.OverlapSphere(transform.position, 2f, LayerMask.GetMask("Character", "Keep"));
            foreach (var collider in colliders) {
                var root = collider.transform.root;
                if (root == transform.root) continue;
                if (handled.Contains(root)) continue;
                collider.SendMessageUpwards("Damage");
                handled.Add(root);
            }
        }
        yield return new WaitForSeconds(SwingTime);
        Animation.SetTrigger("AttackDone");
        Attacking = false;
        AttackCooldown = true;
        yield return new WaitForSeconds(Cooldown);
        AttackCooldown = false;
    }

    void Update() {
        var speed = Aggro ? AggroMoveSpeed : MoveSpeed;
        if (Attacking || AttackCooldown) {
            speed = 0f;
        } else if (Vector3.Distance(transform.position, Target) < TargetRadius) {
            StartCoroutine(Attack());
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
