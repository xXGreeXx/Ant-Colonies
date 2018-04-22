using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt {

    //define global variables
    public Sprite dirtTexture = Resources.Load("dirt", typeof(Sprite)) as Sprite;
    public Sprite grassTexture = Resources.Load("grass", typeof(Sprite)) as Sprite;

    public GameObject self;
    public float moisture;

    //constructor
    public Dirt(int x, int y, int size, bool setGrass)
    {
        self = new GameObject("Dirt " + x + y);
        self.transform.position = new Vector2(x, y);
        self.transform.localScale = new Vector2(11.75F, 11.75F);

        BoxCollider2D collider = self.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.26F, 0.26F);

        SpriteRenderer renderer = self.AddComponent<SpriteRenderer>();
        if (setGrass) renderer.sprite = grassTexture;
        else renderer.sprite = dirtTexture;
        renderer.size = new Vector2(size, size);
    }

    //increase moisture of dirt
    public void IncreaseMoisture(float amount)
    {
        moisture += amount;

        SpriteRenderer renderer = self.GetComponent<SpriteRenderer>();
        
        renderer.color = Color.Lerp(Color.white, Color.gray, moisture);
    }

    //change brigtness
    public Color ChangeColorBrightness(Color color, float correctionFactor)
    {
        float red = color.r;
        float green = color.g;
        float blue = color.b;

        if (correctionFactor < 0)
        {
            correctionFactor = 1 + correctionFactor;
            red *= correctionFactor;
            green *= correctionFactor;
            blue *= correctionFactor;
        }
        else
        {
            red = (255 - red) * correctionFactor + red;
            green = (255 - green) * correctionFactor + green;
            blue = (255 - blue) * correctionFactor + blue;
        }

        return new Color(color.a, (int)red, (int)green, (int)blue);
    }
}
