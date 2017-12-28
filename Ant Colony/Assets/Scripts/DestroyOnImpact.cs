using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour {

    //on collide
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Dirt"))
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

        Destroy(this.gameObject);
    }
}
