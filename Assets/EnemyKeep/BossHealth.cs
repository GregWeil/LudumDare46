using UnityEngine;

public class BossHealth : MonoBehaviour {
    private Vector3 ShakeOffset = Vector3.zero;
    private float ShakeTimer = 0f;

    public GameObject ShakeAudioPrefab;
    public EnemyCannon[] Cannons;

    public int Health;
    public float FallSpeed;
    public float ShakeInterval;
    public float ShakeRadius;
    public GameObject SpawnPrefab;
    public float SpawnInterval;
    private float NextSpawnTime;

    void Update() {
        var shake = false;
        if (Health <= 0) {
            shake = true;
            foreach (var cannon in Cannons) {
                cannon.CooldownTime = float.PositiveInfinity;
            }
            transform.Translate(0, -FallSpeed * Time.deltaTime, 0);
            if (transform.position.y < -15f) Destroy(gameObject);
        } else if (transform.position.y < 0f) {
            shake = true;
            transform.Translate(0, FallSpeed * Time.deltaTime, 0);
            if (transform.position.y > 0f) {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
        if (!shake) {
            if (Time.time > NextSpawnTime) {
                var enemy = Instantiate(SpawnPrefab);
                enemy.name = SpawnPrefab.name;
                enemy.transform.position = transform.position;
                NextSpawnTime = Time.time + SpawnInterval;
            }
        }
        if (!shake) {
            transform.Translate(-ShakeOffset);
            ShakeOffset = Vector3.zero;
        } else if (ShakeTimer < 0f) {
            transform.Translate(-ShakeOffset);
            ShakeOffset = Random.insideUnitSphere * ShakeRadius;
            transform.Translate(ShakeOffset);
            ShakeTimer = ShakeInterval;
            var audio = Instantiate(ShakeAudioPrefab, transform.position, transform.rotation);
            audio.name = ShakeAudioPrefab.name;
            var audioSource = audio.GetComponent<AudioSource>();
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.volume = Health > 0 ? 0.25f : Random.Range(0.25f, 0.5f);
            Destroy(audio, 1f);
        }
        ShakeTimer -= Time.deltaTime;
    }

    void Damage() {
        Health -= 1;
    }
}
