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
    public Sprite redAntWorker = Resources.Load("redAnt", typeof(Sprite)) as Sprite;
    public Sprite redAntQueen = Resources.Load("redAntQueen", typeof(Sprite)) as Sprite;
    public Sprite foodSprite = Resources.Load("food", typeof(Sprite)) as Sprite;

    //data variables
    public GameObject self;
    public bool isQueen;
    public bool produceLarvae = false;
    public AntType type;
    public GameObject goBeingHeld = null;
    public Ant queen = null;
    public Vector2 targetPoint;

    //ant variables
    public float maxHealth;
    public float health;
    public float reproductionValue;
    public float food = 40;
    public float rest;
    public float age;

    //constructor 
    public Ant(float x, float y, bool isQueen, AntType type, Ant queen)
    {
        self = new GameObject("Ant");
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(75, 75);
        if (isQueen) self.layer = 10;
        else self.layer = 8;

        AntCollisionScript script = self.AddComponent<AntCollisionScript>();
        script.parent = this;

        Rigidbody2D rigidBody = self.AddComponent<Rigidbody2D>();
        rigidBody.mass = 5;
        rigidBody.gravityScale = 2;
        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();
        
        this.isQueen = isQueen;
        if (!isQueen) this.queen = queen;
        this.type = type;
        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        renderer.size = new Vector2(50, 25);

        if (type.Equals(AntType.BlackAnt))
        {
            if (isQueen)
            {
                rigidBody.mass = 100;
                food = 500;
                maxHealth = 50;
                health = maxHealth;
                renderer.sprite = blackAntQueen;

                collider.size = new Vector2(0.47F, 0.26F);
            }
            else
            {
                maxHealth = 20;
                health = maxHealth;
                renderer.sprite = blackAntWorker;

                collider.size = new Vector2(0.24F, 0.10F);
            }
        }
        if (type.Equals(AntType.RedAnt))
        {
            if (isQueen)
            {
                rigidBody.mass = 100;
                food = 500;
                maxHealth = 50;
                health = maxHealth;
                renderer.sprite = redAntQueen;

                collider.size = new Vector2(0.39F, 0.26F);
            }
            else
            {
                maxHealth = 20;
                health = maxHealth;
                renderer.sprite = redAntWorker;

                collider.size = new Vector2(0.20F, 0.11F);
            }
        }

        targetPoint = new Vector2(Random.Range(-75, 75), Random.Range(-75, -20));
    }

    //tell ant to dig
    public void Dig()
    {
        foreach (Dirt d in MainGameHandler.dirtBlocks)
        {
            if (Vector2.Distance(d.self.transform.position, self.transform.position) < 5F)
            {
                GameObject.Destroy(d.self, 1);
                MainGameHandler.dirtBlocks.Remove(d);
                break;
            }
        }
    }

    //pickup food
    public void PickupFood()
    {
        if (goBeingHeld == null)
        {
            GameObject foodGo = new GameObject("Food");
            foodGo.transform.SetParent(self.transform);
            foodGo.transform.localScale = new Vector2(1, 1);
            foodGo.transform.localPosition = new Vector2(-0.266F, 0.007F);

            SpriteRenderer renderer = foodGo.AddComponent<SpriteRenderer>();
            renderer.sprite = foodSprite;

            goBeingHeld = foodGo;
        }
    }

    //conusme food
    public void ConsumeFood()
    {
        food += 100;
        GameObject.Destroy(goBeingHeld);
        goBeingHeld = null;
    }

    //decide new point
    public Vector2 DecideNewPoint()
    {
        Vector2 pointToReturn = new Vector2(Random.Range(-75, 75), Random.Range(-75, -20));
        float valueFood = food;
        if (queen.self != null) valueFood = queen.food;

        if (valueFood <= 40)
        {
            if (goBeingHeld == null)
            {
                if (MainGameHandler.leaves.Count > 0)
                {
                    pointToReturn = MainGameHandler.leaves[Random.Range(0, MainGameHandler.leaves.Count)].self.transform.position;
                }
            }
            else
            {
                if (food <= 15)
                {
                    ConsumeFood();
                }
                else
                {
                    if (queen.self != null)
                    {
                        pointToReturn = queen.self.transform.position;
                    }
                    else
                    {
                        ConsumeFood();
                    }
                }
            }
        }

        return pointToReturn;
    }
}
