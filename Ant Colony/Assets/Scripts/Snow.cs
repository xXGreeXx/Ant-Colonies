using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour {

    //define global variables
    public Sprite snowTexture = Resources.Load("snow", typeof(Sprite)) as Sprite;

    public GameObject self;

    //constructor
    public Snow(float x, float y, int size)
    {
        self = new GameObject("Snow");
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(45, 45);

        Rigidbody2D rigid = self.AddComponent<Rigidbody2D>();
        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.10F, 0.10F);

        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        renderer.sprite = snowTexture;
        renderer.size = new Vector2(size, size);
    }
}
