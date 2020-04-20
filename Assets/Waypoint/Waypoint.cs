using UnityEngine;

public class Waypoint : MonoBehaviour {
    public SpriteRenderer Sprite;
    public float RiseSpeed;
    public float FallSpeed;

    [HideInInspector]
    public bool Active = true;

    void Start() {
        foreach (var waypoint in GameObject.FindObjectsOfType<Waypoint>()) {
            if (waypoint == this) continue;
            waypoint.Active = false;
        }
        Sprite.transform.Rotate(Vector3.up, Random.Range(-10f, 10f));
        Sprite.flipX = Random.value >= 0.5f;
    }

    void Update() {
        if (Active) {
            var top = transform.position + Vector3.up * 3;
            if (Physics.OverlapCapsule(transform.position, top, 1f, LayerMask.GetMask("Keep")).Length > 0) {
                Active = false;
            }
        }
        if (Active) {
            Sprite.transform.localPosition = Vector3.MoveTowards(Sprite.transform.localPosition, Vector3.zero, RiseSpeed * Time.deltaTime);
        } else {
            Sprite.transform.localPosition = Vector3.MoveTowards(Sprite.transform.localPosition, Vector3.down * 5f, FallSpeed * Time.deltaTime);
            if (Sprite.transform.localPosition.y < -3f) Destroy(gameObject);
        }
    }
}
