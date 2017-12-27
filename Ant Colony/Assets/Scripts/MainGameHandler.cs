using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameHandler : MonoBehaviour {

    //enums
    public enum Weather
    {
        Sunny,
        Rainy,
        Cloudy,
        Snowy
    }

    public enum Season
    {
        Summer,
        Winter,
        Fall,
        Spring
    }

    //define global variables
    public static List<Dirt> dirtBlocks = new List<Dirt>();
    public static List<Ant> ants = new List<Ant>();
    public static List<Larva> larvae = new List<Larva>();
    public static List<Leaf> leaves = new List<Leaf>();

    public static float antSpeed = 0.5F;
    public static float ageOfEggToHatch = 50;
    public static float antLifeExpectancy = 500;

    //time/weather system
    public Season currentSeason;
    public Weather currentWeather;
    public int currentHour = 0;
    public int currentMinute = 0;
    public int currentDay = 0;
    public int currentMonth;

    public int currentTemperature;

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
        for (int i = 0; i < 3; i++)
        {
            ants.Add(new Ant(Random.Range(-100, 100), 0, true, Ant.AntType.BlackAnt));
        }
    }

    //add leaves
    private void AddLeaves()
    {
        if (Random.Range(0, 450) == 50)
        {
            leaves.Add(new Leaf(Random.Range(-75, 75), 75, 100));
        }
    }

	//update
	void Update () {

        AddLeaves();

        foreach (Ant ant in ants)
        {
            bool breakLoop = SimulateAnt(ant);

            if (breakLoop) break;
        }

        foreach (Larva larva in larvae)
        {
            bool breakLoop = SimulateLarva(larva);

            if (breakLoop) break;
        }
	}

    //simulate ant
    public bool SimulateAnt(Ant selfObject)
    {
        //handle needs
        selfObject.reproductionValue++;
        selfObject.food -= 0.05F;

        if (selfObject.food <= 0) selfObject.health -= 1;
        if (selfObject.health <= 0)
        {
            MainGameHandler.ants.Remove(selfObject);
            GameObject.Destroy(selfObject.self);
            return true;
        }

        //handle movement etc
        if (selfObject.produceLarvae)
        {
            if (selfObject.reproductionValue >= 50 && selfObject.food - 40 > 0)
            {
                selfObject.food -= 40;
                MainGameHandler.larvae.Add(new Larva(selfObject.self.transform.position.x, selfObject.self.transform.position.y, selfObject.type));

                selfObject.reproductionValue = 0;
            }
        }
        else
        {
            if (Mathf.Abs(selfObject.self.transform.position.x - selfObject.targetPoint.x) < MainGameHandler.antSpeed * 5 && Mathf.Abs(selfObject.self.transform.position.y - selfObject.targetPoint.y) < MainGameHandler.antSpeed * 5)
            {
                if (selfObject.isQueen)
                {
                    selfObject.produceLarvae = true;
                    selfObject.targetPoint = new Vector2(selfObject.self.transform.position.x - 3, selfObject.self.transform.position.y);
                    selfObject.self.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    selfObject.targetPoint = new Vector2(Random.Range(-75, 75), Random.Range(-75, -20));
                }
            }
            else
            {
                float distanceX = selfObject.targetPoint.x - selfObject.self.transform.position.x;
                float distanceY = selfObject.targetPoint.y - selfObject.self.transform.position.y;

                if (distanceX > MainGameHandler.antSpeed) distanceX = MainGameHandler.antSpeed;
                if (distanceY > MainGameHandler.antSpeed) distanceY = MainGameHandler.antSpeed;

                if (distanceX < -MainGameHandler.antSpeed) distanceX = -MainGameHandler.antSpeed;
                if (distanceY < -MainGameHandler.antSpeed) distanceY = -MainGameHandler.antSpeed;

                selfObject.self.GetComponent<Rigidbody2D>().MovePosition(selfObject.self.transform.position + new Vector3(distanceX, distanceY, 0));
                float angle = Mathf.Atan2(-distanceY, -distanceX) * Mathf.Rad2Deg;
                selfObject.self.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            if (Mathf.Abs(selfObject.self.transform.position.x - selfObject.targetPoint.x) < 1 || Mathf.Abs(selfObject.self.transform.position.y - selfObject.targetPoint.y) < 1)
            {
                selfObject.Dig();
            }
        }

        return false;
    }

    //simulate larva
    public bool SimulateLarva(Larva selfObject)
    {
        selfObject.age++;

        if (selfObject.age >= 50)
        {
            MainGameHandler.ants.Add(new Ant(selfObject.self.transform.position.x, selfObject.self.transform.position.y, false, selfObject.type));
            MainGameHandler.larvae.Remove(selfObject);
            GameObject.Destroy(selfObject.self);
            return true;
        }

        return false;
    }
}
