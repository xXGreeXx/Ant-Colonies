using UnityEngine;

public class Ant : MonoBehaviour{

    //enums
    public enum AntType
    {
        BlackAnt,
        RedAnt,
        GhostAnt,
        CarpenterAnt,
        CrazyAnt,
        RoverAnt,
        PharaohAnt,
    }

    //data variables
    public bool isQueen;
    public bool produceLarvae = false;
    public AntType type;
    public GameObject goBeingHeld = null;
    public Ant queen = null;
    public Vector2 targetPoint;
    public Vector2 pointStored;

    //ant variables
    public float maxHealth;
    public float health;
    public float reproductionValue;
    public float food = 40;
    public float rest;
    public float age;

    //constructor 
    void Start()
    {
        targetPoint = new Vector2(Random.Range(-75, 75), Random.Range(-75, -20));
    }

    //tell ant to dig
    public void Dig()
    {
        foreach (Dirt d in MainGameHandler.dirtBlocks)
        {
            if (Mathf.Abs(Vector2.Distance(d.self.transform.position, this.gameObject.transform.position)) < 4.5F)
            {
                GameObject.Destroy(d.self, 0.5F);
                MainGameHandler.dirtBlocks.Remove(d);

                DirtRubble rubble = new DirtRubble(d.self.transform.position.x, d.self.transform.position.y, 1000);
                MainGameHandler.dirtRubble.Add(rubble);
                if (!isQueen)
                {
                    goBeingHeld = rubble.self;
                    pointStored = targetPoint;
                    targetPoint = new Vector2(d.self.transform.position.x - Random.Range(0, 2) == 2 ? 20 : -20, 0);
                    rubble.self.transform.SetParent(rubble.self.transform);
                }

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
            foodGo.transform.SetParent(this.gameObject.transform);
            foodGo.transform.localScale = new Vector2(1, 1);
            foodGo.transform.localPosition = new Vector2(-0.266F, 0.007F);

            SpriteRenderer renderer = foodGo.AddComponent<SpriteRenderer>();
            renderer.sprite = SpriteManager.foodSprite;

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
        if (queen != null)
        {
            if (queen.gameObject != null) valueFood = queen.food;
        }

        if (valueFood <= 100)
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
                if (goBeingHeld.name.Equals("Food"))
                {
                    if (food <= 15)
                    {
                        ConsumeFood();
                    }
                    else
                    {
                        bool justEatFood = false;

                        if (queen != null)
                        {
                            if (queen.gameObject != null)
                            {
                                pointToReturn = queen.gameObject.transform.position;
                            }
                            else justEatFood = true;
                        }
                        else justEatFood = true;
                        
                        if(justEatFood)
                        {
                            ConsumeFood();
                        }
                    }
                }
                else if (goBeingHeld.name.Equals("Rubble"))
                {
                    goBeingHeld.transform.SetParent(null);
                    goBeingHeld = null;
                    targetPoint = pointStored;
                }
            }
        }

        return pointToReturn;
    }
}
