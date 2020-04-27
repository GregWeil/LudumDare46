using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {
    public UnityEngine.UI.Text ScoreDisplay;
    public string SceneToLoad;
    public float Timeout;

    void Update() {
        if (Timeout > 0f) {
            Timeout -= Time.deltaTime;
        } else if (Input.anyKeyDown) {
            SceneManager.LoadScene(SceneToLoad);
        }

        if (ScoreDisplay != null) {
            ScoreDisplay.text = EnemyKeepSpawner.ScoreText;
        }
    }
}
