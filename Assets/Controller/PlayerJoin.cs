using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoin : MonoBehaviour {
    public GameObject[] CharacterPrefabs;
    private int JoinIndex = 0;

    void OnPlayerJoined(PlayerInput player) {
        player.gameObject.name = $"Player {JoinIndex + 1}";
        player.GetComponent<PlayerController>().CharacterPrefab = CharacterPrefabs[JoinIndex % CharacterPrefabs.Length];
        JoinIndex += 1;
    }
}
