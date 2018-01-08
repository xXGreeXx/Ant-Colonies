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
    public static List<GameObject> ants = new List<GameObject>();
    public static List<Dirt> dirtBlocks = new List<Dirt>();
    public static List<Larva> larvae = new List<Larva>();
    public static List<Leaf> leaves = new List<Leaf>();
    public static List<RainDrop> rain = new List<RainDrop>();
    public static List<SnowDrop> snow = new List<SnowDrop>();
    public static List<Snow> snowBlocks = new List<Snow>();
    public static List<DirtRubble> dirtRubble = new List<DirtRubble>();

    public static float antSpeed = 0.25F;
    public static float ageOfEggToHatch = 100;
    public static float antLifeExpectancy = 500;
    public static float simulationSpeed = 1;

    //time/weather system
    public Season currentSeason;
    public Weather currentWeather;
    public int currentHour = 0;
    public int currentMinute = 0;
    public int currentDay = 0;
    public int currentMonth = 0;
    public int currentYear = 0;

    public int currentTemperature = 0;

    float timeCycle = 0;

	//start
	void Start () {
        GenerateGround(450, 150, 1000);
        CreateColonies();
        StartBaseWeatherDateTime();
	}
	
    //start base weather/date/time
    private void StartBaseWeatherDateTime()
    {
        currentHour = 12;
        currentMinute = 30;

        currentDay = System.DateTime.Now.Day;
        currentMonth = System.DateTime.Now.Month;
        currentYear = System.DateTime.Now.Year;

        //seasons/weather/temperature
        if (currentMonth <= 3)
        {
            currentSeason = Season.Spring;
            int i = Random.Range(1, 4);
            if (i == 1) currentWeather = Weather.Rainy;
            else if (i == 2) currentWeather = Weather.Cloudy;
            else if (i == 3) currentWeather = Weather.Sunny;

            if (currentWeather.Equals(Weather.Rainy)) currentTemperature = Random.Range(50, 60);
            if (currentWeather.Equals(Weather.Snowy)) currentTemperature = Random.Range(45, 60);
            if (currentWeather.Equals(Weather.Cloudy)) currentTemperature = Random.Range(60, 70);
        }
        else if (currentMonth <= 6)
        {
            currentSeason = Season.Summer;
            int i = Random.Range(1, 5);
            if (i == 1) currentWeather = Weather.Rainy;
            else if (i == 2) currentWeather = Weather.Cloudy;
            else if (i == 3 || i == 4) currentWeather = Weather.Sunny;
            if (currentWeather.Equals(Weather.Rainy)) currentTemperature = Random.Range(65, 80);
            if (currentWeather.Equals(Weather.Cloudy)) currentTemperature = Random.Range(60, 70);
            if (currentWeather.Equals(Weather.Sunny)) currentTemperature = Random.Range(70, 100);
        }
        else if (currentMonth <= 9)
        {
            currentSeason = Season.Fall;
            int i = Random.Range(1, 5);
            if (i == 1) currentWeather = Weather.Rainy;
            else if (i == 2 || i == 3) currentWeather = Weather.Cloudy;
            else if (i == 4) currentWeather = Weather.Sunny;
            if (currentWeather.Equals(Weather.Rainy)) currentTemperature = Random.Range(35, 50);
            if (currentWeather.Equals(Weather.Cloudy)) currentTemperature = Random.Range(40, 55);
            if (currentWeather.Equals(Weather.Sunny)) currentTemperature = Random.Range(50, 65);
        }
        else if (currentMonth <= 12)
        {
            currentSeason = Season.Winter;
            int i = Random.Range(1, 4);
            if (i == 1 || i == 2) currentWeather = Weather.Snowy;
            else if (i == 3) currentWeather = Weather.Sunny;
            if (currentWeather.Equals(Weather.Snowy)) currentTemperature = Random.Range(-10, 32);
            if (currentWeather.Equals(Weather.Sunny)) currentTemperature = Random.Range(30, 40);
        }
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

                Dirt dirt = new Dirt(x, y, size, setGrass);
                if (y > -height / 2 + 3) dirtBlocks.Add(dirt);
            }
        }
    }

    //generate base colonies
    private void CreateColonies()
    {
        ants.Add(CreateAnt(Random.Range(-100, 100), 0, true, Ant.AntType.RedAnt, null));
    }

    //create ant 
    public GameObject CreateAnt(float x, float y, bool isQueen, Ant.AntType type, Ant queen)
    {
        GameObject self = new GameObject("Ant");
        self.transform.parent = GameObject.Find("Canvas").transform;
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(75, 75);
        if (isQueen) self.layer = 10;
        else self.layer = 8;

        Ant selfAnt = self.AddComponent<Ant>();
        AntCollisionScript script = self.AddComponent<AntCollisionScript>();
        script.parent = selfAnt;

        Rigidbody2D rigidBody = self.AddComponent<Rigidbody2D>();
        rigidBody.mass = 1;
        rigidBody.gravityScale = 2;
        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();

        selfAnt.isQueen = isQueen;
        if (!isQueen) selfAnt.queen = queen;
        selfAnt.type = type;
        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        renderer.size = new Vector2(50, 25);

        if (type.Equals(Ant.AntType.BlackAnt))
        {
            if (isQueen)
            {
                rigidBody.mass = 100;
                selfAnt.food = 500;
                selfAnt.maxHealth = 50;
                selfAnt.health = selfAnt.maxHealth;
                renderer.sprite = SpriteManager.blackAntQueen;

                collider.size = new Vector2(0.46F, 0.24F);
            }
            else
            {
                selfAnt.maxHealth = 20;
                selfAnt.health = selfAnt.maxHealth;
                renderer.sprite = SpriteManager.blackAntWorker;

                collider.size = new Vector2(0.24F, 0.10F);
            }
        }
        if (type.Equals(Ant.AntType.RedAnt))
        {
            if (isQueen)
            {
                rigidBody.mass = 100;
                selfAnt.food = 500;
                selfAnt.maxHealth = 50;
                selfAnt.health = selfAnt.maxHealth;
                renderer.sprite = SpriteManager.redAntQueen;

                collider.size = new Vector2(0.39F, 0.17F);
            }
            else
            {
                selfAnt.maxHealth = 20;
                selfAnt.health = selfAnt.maxHealth;
                renderer.sprite = SpriteManager.redAntWorker;

                collider.size = new Vector2(0.20F, 0.11F);
            }
        }

        return self;
    }

    //add rain
    private void AddRain()
    {
        if (Random.Range(0, 10) == 5)
        {
            for (int i = 0; i < Random.Range(50, 150); i++)
            {
                rain.Add(new RainDrop(Random.Range(-150, 150), 75, 100));
            }
        }
    }

    //add snow
    private void AddSnow()
    {
        if (Random.Range(0, 10) == 5)
        {
            for (int i = 0; i < Random.Range(50, 150); i++)
            {
                snow.Add(new SnowDrop(Random.Range(-150, 150), 75, 100));
            }
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

        //update HUD 
        ButtonHandler.tempHud.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Temperature: " + currentTemperature + "°F \n" +
            "Season: " + currentSeason.ToString() + "\n" +
            "Weather: " + currentWeather.ToString() + "\n" +
            "Date: " + currentMonth + "/" + currentDay + "/" + currentYear + "\n" +
            "Time: " + currentHour + ":" + (currentMinute > 10 ? currentMinute.ToString() : "0" + currentMinute);

        //handle time/seasons
        timeCycle += 1 * Time.deltaTime;
        if (timeCycle >= 3)
        {
            //time
            currentMinute++;
            if (currentMinute >= 60)
            {
                currentMinute = 0;
                currentHour++;
            }

            //date
            if (currentHour > 24)
            {
                currentHour = 0;
                currentDay++;
                if (currentDay > 30)
                {
                    currentDay = 0;
                    currentMonth++;
                    if (currentMonth > 12)
                    {
                        currentMonth = 0;
                        currentYear++;
                    }
                }

                //seasons/weather/temperature
                if (currentMonth <= 3)
                {
                    currentSeason = Season.Spring;
                    int i = Random.Range(1, 4);
                    if (i == 1) currentWeather = Weather.Rainy;
                    else if (i == 2) currentWeather = Weather.Cloudy;
                    else if (i == 3) currentWeather = Weather.Sunny;

                    if (currentWeather.Equals(Weather.Rainy)) currentTemperature = Random.Range(50, 60);
                    if (currentWeather.Equals(Weather.Snowy)) currentTemperature = Random.Range(45, 60);
                    if (currentWeather.Equals(Weather.Cloudy)) currentTemperature = Random.Range(60, 70);
                }
                else if (currentMonth <= 6)
                {
                    currentSeason = Season.Summer;
                    int i = Random.Range(1, 5);
                    if (i == 1) currentWeather = Weather.Rainy;
                    else if (i == 2) currentWeather = Weather.Cloudy;
                    else if (i == 3 || i == 4) currentWeather = Weather.Sunny;
                    if (currentWeather.Equals(Weather.Rainy)) currentTemperature = Random.Range(65, 80);
                    if (currentWeather.Equals(Weather.Cloudy)) currentTemperature = Random.Range(60, 70);
                    if (currentWeather.Equals(Weather.Sunny)) currentTemperature = Random.Range(70, 100);
                }
                else if (currentMonth <= 9)
                {
                    currentSeason = Season.Fall;
                    int i = Random.Range(1, 5);
                    if (i == 1) currentWeather = Weather.Rainy;
                    else if (i == 2 || i == 3) currentWeather = Weather.Cloudy;
                    else if (i == 4) currentWeather = Weather.Sunny;
                    if (currentWeather.Equals(Weather.Rainy)) currentTemperature = Random.Range(35, 50);
                    if (currentWeather.Equals(Weather.Cloudy)) currentTemperature = Random.Range(40, 55);
                    if (currentWeather.Equals(Weather.Sunny)) currentTemperature = Random.Range(50, 65);
                }
                else if (currentMonth <= 12)
                {
                    currentSeason = Season.Winter;
                    int i = Random.Range(1, 4);
                    if (i == 1 || i == 2) currentWeather = Weather.Snowy;
                    else if (i == 3) currentWeather = Weather.Sunny;
                    if (currentWeather.Equals(Weather.Snowy)) currentTemperature = Random.Range(-10, 32);
                    if (currentWeather.Equals(Weather.Sunny)) currentTemperature = Random.Range(30, 40);
                }
            }

            timeCycle = 0;
        }

        //handle light
        float combinedColorLerp = 0;

        combinedColorLerp += (Mathf.Abs(14 - currentHour) * 60 + 30) / 370F;
        Camera.main.backgroundColor = Color.Lerp(Color.blue, Color.black, combinedColorLerp);

        //handle game simulation
        AddLeaves();
        if (currentWeather.Equals(Weather.Rainy)) AddRain();
        if (currentWeather.Equals(Weather.Snowy)) AddSnow();
        if (currentTemperature > 35 && snowBlocks.Count > 0) snowBlocks.RemoveAt(Random.Range(0, snowBlocks.Count));

        foreach (GameObject ant in ants)
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
    public bool SimulateAnt(GameObject goGiven)
    {
        Ant selfObject = goGiven.GetComponent<Ant>();

        //handle needs
        selfObject.reproductionValue++;
        selfObject.food -= 0.05F;

        if (selfObject.food <= 0) selfObject.health -= 1;
        if (selfObject.health <= 0)
        {
            MainGameHandler.ants.Remove(selfObject.transform.gameObject);
            GameObject.Destroy(selfObject);
            return true;
        }

        //handle movement etc
        if (selfObject.produceLarvae)
        {
            if (selfObject.reproductionValue >= 120 && selfObject.food - 40 > 30)
            {
                selfObject.food -= 40;
                MainGameHandler.larvae.Add(new Larva(selfObject.transform.position.x, selfObject.transform.position.y, selfObject.type, selfObject));

                selfObject.reproductionValue = 0;
            }
        }
        else
        {
            if (selfObject.food < 40)
            {
                if (!selfObject.isQueen)
                {
                    selfObject.targetPoint = selfObject.DecideNewPoint();
                }
            }

            if (Mathf.Abs(selfObject.transform.position.x - selfObject.targetPoint.x) < MainGameHandler.antSpeed * 2 && Mathf.Abs(selfObject.transform.position.y - selfObject.targetPoint.y) < MainGameHandler.antSpeed * 2)
            {
                if (selfObject.isQueen)
                {
                    selfObject.produceLarvae = true;
                    selfObject.targetPoint = new Vector2(selfObject.transform.position.x - 3, selfObject.transform.position.y);
                    selfObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    selfObject.targetPoint = selfObject.DecideNewPoint();
                }
            }
            else
            {
                float distanceX = selfObject.targetPoint.x - selfObject.transform.position.x;
                float distanceY = selfObject.targetPoint.y - selfObject.transform.position.y;

                if (distanceX > MainGameHandler.antSpeed) distanceX = MainGameHandler.antSpeed;
                if (distanceY > MainGameHandler.antSpeed) distanceY = MainGameHandler.antSpeed;

                if (distanceX < -MainGameHandler.antSpeed) distanceX = -MainGameHandler.antSpeed;
                if (distanceY < -MainGameHandler.antSpeed) distanceY = -MainGameHandler.antSpeed;

                if (distanceY > 0 && selfObject.transform.position.y < 0) distanceY = 0;

                selfObject.GetComponent<Rigidbody2D>().MovePosition(selfObject.transform.position + new Vector3(distanceX, distanceY, 0));

                float angle = Mathf.Atan2(-distanceY, -distanceX) * Mathf.Rad2Deg;
                selfObject.GetComponent<Rigidbody2D>().MoveRotation(angle);
            }

            if (Mathf.Abs(selfObject.transform.position.x - selfObject.targetPoint.x) < antSpeed * 3 
                || Mathf.Abs(selfObject.transform.position.y - selfObject.targetPoint.y) < antSpeed * 3)
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
            MainGameHandler.ants.Add(CreateAnt(selfObject.self.transform.position.x, selfObject.self.transform.position.y, false, selfObject.type,  selfObject.queen));
            MainGameHandler.larvae.Remove(selfObject);
            GameObject.Destroy(selfObject.self);
            return true;
        }

        return false;
    }

    //handle exit
    public static void ExitGame()
    {
        Application.Quit();
    }
}
