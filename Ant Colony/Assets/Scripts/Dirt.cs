using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt {

    //define global variables
    public Sprite dirtTexture = Resources.Load("dirt", typeof(Sprite)) as Sprite;
    public Sprite grassTexture = Resources.Load("grass", typeof(Sprite)) as Sprite;

    public GameObject self;

    //constructor
    public Dirt(int x, int y, int size, bool setGrass)
    {
        self = new GameObject("Dirt " + x + y);
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(75, 75);

        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.26F, 0.26F);

        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        if (setGrass) renderer.sprite = grassTexture;
        else renderer.sprite = dirtTexture;
        renderer.size = new Vector2(size, size);
    }
}
