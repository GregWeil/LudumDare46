using UnityEngine;

public class PlayerController : MonoBehaviour {
    private GameObject Keep;
    private GameObject ActiveCharacter;

    public GameObject CharacterPrefab;
    public GameObject CannonPrefab;

    public float SpawnDistance;

    void Start() {
        Keep = GameObject.Find("Keep");
        ExitKeep();
    }

    public void EnterKeep(GameObject keep) {
        Keep = keep;
        if (ActiveCharacter != null) Destroy(ActiveCharacter);
        ActiveCharacter = Instantiate(CannonPrefab, transform);
        ActiveCharacter.name = CannonPrefab.name;
        ActiveCharacter.transform.position = Keep.transform.position;
    }

    public void ExitKeep() {
        if (ActiveCharacter != null) Destroy(ActiveCharacter);
        ActiveCharacter = Instantiate(CharacterPrefab, transform);
        ActiveCharacter.name = CharacterPrefab.name;
        var spawnOrigin = new Vector3(Keep.transform.position.x, 0, Keep.transform.position.z);
        var spawnOffset = (Camera.main.transform.position - Keep.transform.position);
        spawnOffset.y = 0;
        spawnOffset = Quaternion.Euler(0, Random.Range(-15f, 15f), 0) * spawnOffset.normalized * SpawnDistance;
        ActiveCharacter.transform.position = spawnOrigin + spawnOffset;
    }
}
