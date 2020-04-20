using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCannon : MonoBehaviour {
    public GameObject CannonballPrefab;
    public GameObject Cannon;
    public GameObject Reticle;
    public float MoveSpeed;
    public float RotationSpeed;
    public float ActivationRange;
    public float TrackingTime;
    public float FireDelay;
    public float CooldownTime;

    void Start() {
        StartCoroutine(Cycle());
    }

    public void Update() {
        var offset = Reticle.transform.position - transform.position;
        var angle = Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler(0, -angle, 0);
        var lerp = 1f - Mathf.Pow(1f - RotationSpeed, Time.deltaTime*60f);
        Cannon.transform.rotation = Quaternion.Lerp(Cannon.transform.rotation, targetRotation, lerp);
    }

    IEnumerator Cycle() {
        while (true) {
            Reticle.SetActive(false);
            GameObject target = null;
            Vector3 lastPosition = Vector3.zero;

            while (target == null) {
                var players = GameObject.FindObjectsOfType<PlayerMovement>();
                var potentialTargets = players.Select(player => player.gameObject).Prepend(GameObject.Find("Keep"));
                foreach (var potential in potentialTargets) {
                    var offset = potential.transform.position - transform.position;
                    offset.y = 0;
                    if (offset.magnitude < ActivationRange) {
                        target = potential.gameObject;
                        lastPosition = Vector3.Scale(target.transform.position, new Vector3(1, 0, 1));
                        break;
                    }
                }
                yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
            }

            Reticle.transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
            Reticle.SetActive(true);
            var endTracking = Time.time + TrackingTime;
            while (Time.time < endTracking) {
                if (target != null) {
                    lastPosition = Vector3.Scale(target.transform.position, new Vector3(1, 0, 1));
                }
                var lerp = 1f - Mathf.Pow(1f - MoveSpeed, Time.deltaTime*60f);
                Reticle.transform.position = Vector3.Lerp(Reticle.transform.position, lastPosition, lerp);
                yield return null;
            }

            yield return new WaitForSeconds(FireDelay);

            var gameObject = Instantiate(CannonballPrefab);
            gameObject.name = CannonballPrefab.name;
            gameObject.transform.position = Cannon.transform.position;
            var cannonball = gameObject.GetComponent<Cannonball>();
            cannonball.TargetPosition = Reticle.transform.position;
            cannonball.IgnoreObject = transform.root.gameObject;

            yield return new WaitForSeconds(cannonball.Duration);

            Reticle.SetActive(false);
            yield return new WaitForSeconds(CooldownTime);
        }
    }
}
