using System.Collections;
using UnityEngine;

public class OrcHealth : MonoBehaviour {
    bool Dead = false;

    public GameObject[] DropPrefabs;
    public float DropRate;

    public float FallDelay;
    public float FallSpeed;

    IEnumerator DeathAnimation() {
        yield return new WaitForSeconds(FallDelay);
        while (transform.position.y > -2f) {
            transform.Translate(0, -FallSpeed * Time.deltaTime, 0);
            yield return null;
        }
        Destroy(gameObject);
    }

    void Damage() {
        if (Dead) return;
        GetComponent<OrcMovement>().enabled = false;
        GetComponentInChildren<Animator>().SetTrigger("Dead");
        StartCoroutine(DeathAnimation());
        if (Random.value < DropRate) {
            var prefab = DropPrefabs[Random.Range(0, DropPrefabs.Length)];
            var drop = Instantiate(prefab);
            drop.name = prefab.name;
            drop.transform.position = transform.position + Vector3.back;
        }
        Dead = true;
    }
}
