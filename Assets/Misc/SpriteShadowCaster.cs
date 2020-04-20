using UnityEngine;
using UnityEngine.Rendering;

public class SpriteShadowCaster : MonoBehaviour {
    private SpriteRenderer Source;
    private SpriteRenderer Sprite;

    void Start()  {
        Source = transform.parent.GetComponent<SpriteRenderer>();
        Sprite = GetComponent<SpriteRenderer>();
        Sprite.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        Sync();
    }

    void LateUpdate() {
        Sync();
    }

    void Sync() {
        Sprite.sprite = Source.sprite;
        Sprite.flipX = Source.flipX;
        Sprite.flipY = Source.flipY;
    }
}
