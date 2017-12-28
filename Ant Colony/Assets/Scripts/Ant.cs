using UnityEngine;

public class Ant {

    //enums
    public enum AntType
    {
        BlackAnt,
        RedAnt
    }

    //define global variables
    public Sprite blackAntWorker = Resources.Load("blackAnt", typeof(Sprite)) as Sprite;
    public Sprite blackAntQueen = Resources.Load("blackAntQueen", typeof(Sprite)) as Sprite;

    //data variables
    public GameObject self;
    public bool isQueen;
    public bool produceLarvae = false;
    public AntType type;
    public GameObject goBeingHeld = null;
    public Vector2 targetPoint;

    //ant variables
    public float maxHealth;
    public float health;
    public float reproductionValue;
    public float food = 50;
    public float rest;
    public float age;

    //constructor 
    public Ant(float x, float y, bool isQueen, AntType type)
    {
        self = new GameObject("Ant");
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(75, 75);
        self.layer = 8;

        Rigidbody2D rigidBody = self.AddComponent<Rigidbody2D>();
        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.47F, 0.26F);
        
        this.isQueen = isQueen;
        this.type = type;
        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        renderer.size = new Vector2(50, 25);

        if (type.Equals(AntType.BlackAnt))
        {
            if (isQueen)
            {
                food = 500;
                maxHealth = 50;
                health = maxHealth;
                renderer.sprite = blackAntQueen;
            }
            else
            {
                maxHealth = 20;
                health = maxHealth;
                renderer.sprite = blackAntWorker;
            }
        }

        targetPoint = new Vector2(Random.Range(-75, 75), Random.Range(-75, -20));
    }

    //tell ant to dig
    public void Dig()
    {
        foreach (Dirt d in MainGameHandler.dirtBlocks)
        {
            if (Vector2.Distance(d.self.transform.position, self.transform.position) < 4.5F)
            {
                GameObject.Destroy(d.self, 1);
                MainGameHandler.dirtBlocks.Remove(d);
                break;
            }
        }
    }
}
