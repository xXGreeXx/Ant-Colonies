using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larva {

    //define global variables
    public Sprite larvaTexture = Resources.Load("larva", typeof(Sprite)) as Sprite;

    public GameObject self;
    public int age;
    public Ant.AntType type;
    public Ant queen;

    //constructor
    public Larva(float x, float y, Ant.AntType type, Ant queen)
    {
        self = new GameObject("Dirt " + x + y);
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(25, 25);
        self.layer = 9;

        Rigidbody2D rigidBody = self.AddComponent<Rigidbody2D>();
        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.3F, 0.26F);

        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        renderer.sprite = larvaTexture;
        renderer.size = new Vector2(5, 5);

        this.queen = queen;
        this.type = type;
    }
}
