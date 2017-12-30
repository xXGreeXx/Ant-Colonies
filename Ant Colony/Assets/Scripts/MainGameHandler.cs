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
    public static List<RainDrop> rain = new List<RainDrop>();
    public static List<SnowDrop> snow = new List<SnowDrop>();

    public static float antSpeed = 0.5F;
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
            if (currentWeather.Equals(Weather.Cloudy)) currentTemperature = Random.Range(45, 60);
            if (currentWeather.Equals(Weather.Sunny)) currentTemperature = Random.Range(60, 70);
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

                dirtBlocks.Add(new Dirt(x, y, size, setGrass));
            }
        }
    }

    //generate base colonies
    private void CreateColonies()
    {
        for (int i = 0; i < 3; i++)
        {
            ants.Add(new Ant(Random.Range(-100, 100), 0, true, Ant.AntType.BlackAnt, null));
        }
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
                    if (currentWeather.Equals(Weather.Cloudy)) currentTemperature = Random.Range(45, 60);
                    if (currentWeather.Equals(Weather.Sunny)) currentTemperature = Random.Range(60, 70);
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
            if (selfObject.reproductionValue >= 120 && selfObject.food - 40 > 0)
            {
                selfObject.food -= 40;
                MainGameHandler.larvae.Add(new Larva(selfObject.self.transform.position.x, selfObject.self.transform.position.y, selfObject.type, selfObject));

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
                    selfObject.targetPoint = selfObject.DecideNewPoint();
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
            MainGameHandler.ants.Add(new Ant(selfObject.self.transform.position.x, selfObject.self.transform.position.y, false, selfObject.type,  selfObject.queen));
            MainGameHandler.larvae.Remove(selfObject);
            GameObject.Destroy(selfObject.self);
            return true;
        }

        return false;
    }
}
