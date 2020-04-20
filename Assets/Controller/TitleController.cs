using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {
    public string SceneToLoad;

    void Update() {
        if (Input.anyKeyDown) {
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
