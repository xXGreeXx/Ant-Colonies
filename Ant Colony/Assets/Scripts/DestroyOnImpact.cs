using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour {

    //on collide
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Dirt"))
        {
            if (this.name.Equals("Drop"))
            {
                foreach (Dirt d in MainGameHandler.dirtBlocks)
                {
                    if (d.self.name.Equals(collision.gameObject.name))
                    {
                        d.IncreaseMoisture(0.1F);
                        break;
                    }
                }
            }
        }

        if (this.name.Equals("Flake") && !collision.gameObject.name.Equals("Flake")) MainGameHandler.snowBlocks.Add(new Snow(collision.transform.position.x, collision.transform.position.y, 1000));

        Destroy(this.gameObject);
    }
}
