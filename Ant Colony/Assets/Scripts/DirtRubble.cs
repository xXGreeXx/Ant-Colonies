using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtRubble {

    //define global variables
    public Sprite dirtTexture = Resources.Load("dirtRubble", typeof(Sprite)) as Sprite;

    public GameObject self;

    //constructor
    public DirtRubble(float x, float y, int size)
    {
        self = new GameObject("Rubble");
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(45, 45);

        Rigidbody2D rigid = self.AddComponent<Rigidbody2D>();
        rigid.mass = 200;
        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.10F, 0.10F);

        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        renderer.sprite = dirtTexture;
        renderer.size = new Vector2(size, size);
    }
}
