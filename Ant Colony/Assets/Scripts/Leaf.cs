using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf {

    //define global variables
    public Sprite leafTexture = Resources.Load("leaf", typeof(Sprite)) as Sprite;

    public GameObject self;
    public int foodValue = 200;

    //constructor
    public Leaf(int x, int y, int size)
    {
        self = new GameObject("Leaf");
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(150, 150);

        Rigidbody2D rigid = self.AddComponent<Rigidbody2D>();
        rigid.mass = 20;
        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.75F, 0.25F);

        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        renderer.sprite = leafTexture;
        renderer.size = new Vector2(size, size);
    }
}
