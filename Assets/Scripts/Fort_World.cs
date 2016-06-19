using UnityEngine;
using System.Collections;

public class Fort_World : MonoBehaviour {
    public bool IsAlly;
    public Sprite AllySprite;
    public Sprite EnemySprite;
    public int FortNum;

	// Use this for initialization
	void Start () {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) {
            renderer.sprite = IsAlly ? AllySprite : EnemySprite;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
