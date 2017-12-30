using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntCollisionScript : MonoBehaviour {

    public Ant parent;

    void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.gameObject.name;

        if (name.Equals("Leaf"))
        {
            parent.PickupFood();
            parent.DecideNewPoint();
        }
        if (name.Equals("Ant"))
        {
            if (parent.queen != null)
            {
                if (parent.goBeingHeld != null)
                {
                    GameObject.Destroy(parent.goBeingHeld);
                    parent.goBeingHeld = null;
                    parent.queen.food += 100;
                    parent.DecideNewPoint();
                }
            }
        }
    }
}
