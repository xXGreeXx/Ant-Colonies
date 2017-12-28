using UnityEngine;

public class RainDrop {

    //define global variables
    public Sprite rainTexture = Resources.Load("raindrop", typeof(Sprite)) as Sprite;

    public GameObject self;

    //constructor
    public RainDrop(int x, int y, int size)
    {
        self = new GameObject("Drop");
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(50, 50);
        self.AddComponent<DestroyOnImpact>();

        Rigidbody2D rigid = self.AddComponent<Rigidbody2D>();
        rigid.mass = 1;
        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.159F, 0.25F);

        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        renderer.sprite = rainTexture;
        renderer.size = new Vector2(size, size);
    }
}
