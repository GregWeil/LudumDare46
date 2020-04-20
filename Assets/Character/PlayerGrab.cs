using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrab : MonoBehaviour {
    private Animator Animation;

    public float Range;
    public float GrabSpeed;
    public float CarryHeight;

    private Pickup HeldItem;

    void Start() {
        Animation = GetComponentInChildren<Animator>();
    }

    void Update() {
        if (HeldItem != null) {
            var targetPosition = transform.TransformPoint(0, CarryHeight, 0);
            HeldItem.transform.position = Vector3.MoveTowards(HeldItem.transform.position, targetPosition, GrabSpeed * Time.deltaTime);
        }
        Animation.SetBool("Carrying", HeldItem != null);
    }

    void Grab() {
        if (HeldItem == null) {
            var colliders = Physics.OverlapSphere(transform.position, Range, LayerMask.GetMask("Pickup"));
            Pickup closest = null;
            var closestDistance = float.PositiveInfinity;
            foreach (var collider in colliders) {
                var pickup = collider.GetComponentInParent<Pickup>();
                var distance = (collider.ClosestPoint(transform.position) - transform.position).magnitude;
                if (pickup != null && distance < closestDistance && !pickup.Held) {
                    closest = pickup;
                    closestDistance = distance;
                }
            }
            if (closest != null) {
                HeldItem = closest;
                HeldItem.Held = true;
            }
        }
    }

    void Drop() {
        if (HeldItem != null) {
            HeldItem.SendMessage("Drop");
            HeldItem = null;
        }
    }

    void OnGrab(InputValue value) {
        if (value.isPressed && isActiveAndEnabled) {
            if (HeldItem != null) {
                Drop();
            } else {
                Grab();
            }
        }
    }

    void EnterKeep(GameObject keep) {
        if (HeldItem != null) {
            HeldItem.Apply(keep.GetComponent<KeepController>());
            Drop();
        }
    }

    void Dead() {
        Drop();
        enabled = false;
    }

    void OnDestroy() {
        Drop();
    }
}
