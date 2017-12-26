using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameHandler : MonoBehaviour {

    //define global variables
    public static List<Dirt> dirtBlocks = new List<Dirt>();
    public static List<Ant> ants = new List<Ant>();

    public static float antSpeed = 0.5F;

	//start
	void Start () {
        GenerateGround(450, 150, 1000);
        CreateColonies();
	}
	
    //generate ground
    private void GenerateGround(int width, int height, int size)
    {
        for (int y = -height / 2; y < 0; y += 3)
        {
            for (int x = -width / 2; x < width / 2; x += 3)
            {
                bool setGrass = false;
                if (y == -3) setGrass = true;

                dirtBlocks.Add(new Dirt(x, y, size, setGrass));
            }
        }
    }

    //generate base colonies
    private void CreateColonies()
    {
        for (int i = 0; i < 1; i++)
        {
            ants.Add(new Ant(Random.Range(-100, 100), 0, true, Ant.AntType.BlackAnt));
        }
    }

	//update
	void Update () {

        foreach (Ant ant in ants)
        {
            ant.Simulate();
        }
	}
}
