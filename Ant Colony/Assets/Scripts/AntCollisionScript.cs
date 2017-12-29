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
    }
}
