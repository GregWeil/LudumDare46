using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    private bool Dead = false;

    IEnumerator Die() {
        Dead = true;
        SendMessage("Dead");
        yield return new WaitForSeconds(1f);
        SendMessageUpwards("ExitKeep");
    }

    void Damage() {
        if (!Dead) {
            StartCoroutine(Die());
        }
    }
}
