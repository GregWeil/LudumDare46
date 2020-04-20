using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteSwap : MonoBehaviour {
    [System.Serializable]
    public class SwapEntry {
        public Sprite Base;
        public Sprite Override;
    }

    public SwapEntry[] Replacements;
    private Dictionary<Sprite, Sprite> Lookup;
    private SpriteRenderer Renderer;

    void Start() {
        Lookup = Replacements.ToDictionary(entry => entry.Base, entry => entry.Override);
        Renderer = GetComponent<SpriteRenderer>();
    }
    
    void LateUpdate() {
        if (Lookup.ContainsKey(Renderer.sprite)) {
            Renderer.sprite = Lookup[Renderer.sprite];
        }
    }
}
