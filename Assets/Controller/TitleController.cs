﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {
    void Update() {
        if (Input.anyKeyDown) {
            SceneManager.LoadScene("MainScene");
        }
    }
}
