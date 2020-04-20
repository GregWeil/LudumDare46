using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyKeepSpawner : MonoBehaviour {
    private List<GameObject> ActiveKeeps = new List<GameObject>();
    private int KeepsDestroyed = 0;

    public UnityEngine.UI.Text ScoreDisplay;
    public GameObject EnemyKeepPrefab;
    public float PlayerDistance;
    public float KeepDistance;
    public float SpawnRange;

    void Update() {
        KeepsDestroyed += ActiveKeeps.RemoveAll(keep => keep == null);
        var desired = Mathf.RoundToInt(7f - 6f / (0.1f * KeepsDestroyed + 1f));
        if (ActiveKeeps.Count < desired) {
            SpawnKeep();
        }
        ScoreDisplay.text = KeepsDestroyed > 0 ? $"{KeepsDestroyed}" : "";
    }

    Vector3 GetFallbackPosition(Vector3 avoidPosition) {
        var spawnDirection = Quaternion.LookRotation(Vector3.zero - avoidPosition, Vector3.up) * Quaternion.Euler(0, Random.Range(-30f, 30f), 0);
        return avoidPosition + spawnDirection * Vector3.forward * PlayerDistance;
    }

    void SpawnKeep() {
        var playerKeep = GameObject.Find("Keep");
        var spawnLocation = GetFallbackPosition(playerKeep.transform.position);
        for (var i = 0; i < 10; ++i) {
            var x = Random.Range(-SpawnRange, +SpawnRange);
            var z = Random.Range(-SpawnRange, +SpawnRange);
            var location = new Vector3(x, 0, z);
            if (Vector3.Distance(location, playerKeep.transform.position) < PlayerDistance) continue;
            if (ActiveKeeps.Any(active => Vector3.Scale(active.transform.position - location, new Vector3(1, 0, 1)).magnitude < KeepDistance)) continue;
            spawnLocation = location;
            break;
        }
        var keep = Instantiate(EnemyKeepPrefab);
        keep.name = EnemyKeepPrefab.name;
        keep.transform.position = new Vector3(spawnLocation.x, -15f, spawnLocation.z);
        keep.transform.Rotate(0, Random.Range(0, 180f), 0);
        ActiveKeeps.Add(keep);
    }
}
