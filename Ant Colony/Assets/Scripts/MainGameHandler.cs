using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameHandler : MonoBehaviour {

    //define global variables
    public List<Dirt> dirtBlocks = new List<Dirt>();

	//start
	void Start () {
        GenerateGround(500, 150, 1000);
	}
	
    //generate ground
    private void GenerateGround(int width, int height, int size)
    {
        for (int x = -width / 2; x < width / 2; x += 3)
        {
            for (int y = -height / 2; y < 0; y+= 3)
            {
                dirtBlocks.Add(new Dirt(x, y, size));
            }
        }
    }

	//update
	void Update () {
		
	}
}
