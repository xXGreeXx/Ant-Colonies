using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt {

    //define global variables
    public Sprite dirtTexture = Resources.Load("dirt", typeof(Sprite)) as Sprite;

    public GameObject self;

    public Dirt(int x, int y, int size)
    {
        self = new GameObject("Dirt " + x + y);
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(75, 75);

        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        renderer.sprite = dirtTexture;
        renderer.size = new Vector2(size, size);
    }
}
