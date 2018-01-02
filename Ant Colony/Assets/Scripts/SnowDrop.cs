using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowDrop : MonoBehaviour {

    //define global variables
    public Sprite snowTexture = Resources.Load("snowflake", typeof(Sprite)) as Sprite;

    public GameObject self;

    //constructor
    public SnowDrop(int x, int y, int size)
    {
        self = new GameObject("Flake");
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(Random.Range(10, 30), Random.Range(10, 30));
        self.AddComponent<DestroyOnImpact>();

        Rigidbody2D rigid = self.AddComponent<Rigidbody2D>();
        rigid.mass = 1;
        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.159F, 0.25F);

        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        renderer.sprite = snowTexture;
        renderer.size = new Vector2(size, size);
    }
}
