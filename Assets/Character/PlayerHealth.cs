using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    private bool Dead = false;

    IEnumerator Die() {
        Dead = true;
        SendMessage("Dead");
        transform.RotateAround(transform.TransformPoint(0, -1, 0), Vector3.forward, 90);
        yield return new WaitForSeconds(1f);
        SendMessageUpwards("ExitKeep");
    }

    void Damage() {
        if (!Dead) {
            StartCoroutine(Die());
        }
    }
}
