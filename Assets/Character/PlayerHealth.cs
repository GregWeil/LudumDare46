using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    private bool Dead = false;

    public float RespawnTime;

    IEnumerator Die() {
        Dead = true;
        SendMessage("Dead");
        GetComponentInChildren<Animator>().SetTrigger("Dead");
        yield return new WaitForSeconds(RespawnTime);
        SendMessageUpwards("ExitKeep");
    }

    void Damage() {
        if (!Dead) {
            StartCoroutine(Die());
        }
    }
}
