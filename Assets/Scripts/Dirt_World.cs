using UnityEngine;
using System.Collections;

public class Dirt_World : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    private bool highlighted { get; set; }
    public bool Highlighted {
        get {
            return highlighted;
        }
        set {
            highlighted = value;
            if (value) {
                spriteRenderer.color = Color.yellow;
            } else {
                spriteRenderer.color = Color.white;
            }
        }
    }

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
}
