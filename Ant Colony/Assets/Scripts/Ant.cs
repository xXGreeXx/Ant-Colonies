using System.Collections;
using System.Collections.Generic;
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

    public GameObject self;
    public bool isQueen;
    public AntType type;
    public GameObject goBeingHeld = null;

    public Vector2 targetPoint;

    //constructor 
    public Ant(int x, int y, bool isQueen, AntType type)
    {
        self = new GameObject("Ant");
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(75, 75);

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
                renderer.sprite = blackAntQueen;
            }
            else
            {
                renderer.sprite = blackAntWorker;
            }
        }

        targetPoint = new Vector2(x, y);
    }

    //simulate ant
    public void Simulate()
    {
        Debug.Log(Mathf.Abs(self.transform.position.x - targetPoint.x) + " " + Mathf.Abs(self.transform.position.y - targetPoint.y));
        if (Mathf.Abs(self.transform.position.x - targetPoint.x) < MainGameHandler.antSpeed * 5 && Mathf.Abs(self.transform.position.y - targetPoint.y) < MainGameHandler.antSpeed * 5)
        {
            targetPoint = new Vector2(Random.Range(-75, 75), Random.Range(-50, 0));
        }
        else
        {
            float distanceX = targetPoint.x - self.transform.position.x;
            float distanceY = targetPoint.y - self.transform.position.y;

            if (distanceX > MainGameHandler.antSpeed) distanceX = MainGameHandler.antSpeed;
            if (distanceY > MainGameHandler.antSpeed) distanceY = MainGameHandler.antSpeed;

            if (distanceX < -MainGameHandler.antSpeed) distanceX = -MainGameHandler.antSpeed;
            if (distanceY < -MainGameHandler.antSpeed) distanceY = -MainGameHandler.antSpeed;

            self.GetComponent<Rigidbody2D>().MovePosition(self.transform.position + new Vector3(distanceX, distanceY, 0));
            float angle = Mathf.Atan2(-distanceY, -distanceX) * Mathf.Rad2Deg;
            self.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Mathf.Abs(self.transform.position.x - targetPoint.x) < 1 || Mathf.Abs(self.transform.position.y - targetPoint.y) < 1)
        {
            Dig();
        }
    }

    //tell ant to dig
    private void Dig()
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
