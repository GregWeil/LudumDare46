using UnityEngine;

public class EventAudio : MonoBehaviour {
    private AudioSource Sound;

    void Start() {
        Sound = GetComponent<AudioSource>();
    }
    
    void PlayAudio() {
        Sound.Play();
    }
}
