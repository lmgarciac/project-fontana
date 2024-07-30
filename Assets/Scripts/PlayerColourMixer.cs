using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerColourMixer : MonoBehaviour
{
    public static Queue<Color> mainPalette;

    void Start()
    {
        mainPalette = new Queue<Color>();

        //Adding colors into Queue and into GlobalManager
        AddColor(Color.red, mainPalette);
        AddColor(Color.yellow, mainPalette);
        AddColor(Color.blue, mainPalette);

        GlobalManager.Instance.CurrentPaletteColor1 = Color.red;
        GlobalManager.Instance.CurrentPaletteColor2 = Color.yellow;
        GlobalManager.Instance.CurrentPaletteColor3 = Color.blue;
        GlobalManager.Instance.CurrentPaletteColorResult = Color.white;

        GlobalManager.Instance.CurrentColorType = ColorType.White;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddColor(Color.red, mainPalette);
            StorePaletteColors(mainPalette);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddColor(Color.yellow, mainPalette);
            StorePaletteColors(mainPalette);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddColor(Color.blue, mainPalette);
            StorePaletteColors(mainPalette);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Color resultColor = DetermineColor(mainPalette);
            
            //Rework if needed
            GlobalManager.Instance.CurrentPaletteColorResult = resultColor;
        }
    }

    private void AddColor(Color paletteColor, Queue<Color> paletteToAdd)
    {
        if (paletteToAdd.Count == 3) //3 is the maximum size
        {
            paletteToAdd.Dequeue();
        }
        paletteToAdd.Enqueue(paletteColor);
    }

    private void StorePaletteColors(Queue<Color> paletteToStore)
    {
        Color[] paletteArray = paletteToStore.ToArray(); //Dont like how I made this, rework later

        GlobalManager.Instance.CurrentPaletteColor1 = paletteArray[0];
        GlobalManager.Instance.CurrentPaletteColor2 = paletteArray[1];
        GlobalManager.Instance.CurrentPaletteColor3 = paletteArray[2];
    }

    private Color DetermineColor(Queue<Color> palette)
    {   
        int countR = 0;
        int countY = 0;
        int countB = 0;

        foreach (Color color in palette)
        {
            if (color == Color.red) countR++;
            else if (color == Color.yellow) countY++;
            else if (color == Color.blue) countB++;
        }

        if (countR == 3) //This wont work the first time when there are less than 3 colors in palette
        {
            GlobalManager.Instance.CurrentColorType = ColorType.Red;
            return Color.red;
        } 
        else if (countY == 3)
        {
            GlobalManager.Instance.CurrentColorType = ColorType.Yellow;
            return Color.yellow;
        } 
        else if (countB == 3)
        {
            GlobalManager.Instance.CurrentColorType = ColorType.Blue;
            return Color.blue;
        } 
        else if ((countR == 2 && countY == 1) || (countY == 2 && countR == 1))
        {
            GlobalManager.Instance.CurrentColorType = ColorType.Orange;
            return new Color(1, 0.2f, 0, 1); //Orange
        }  
        else if ((countY == 2 && countB == 1) || (countB == 2 && countY == 1))
        {
            GlobalManager.Instance.CurrentColorType = ColorType.Green;
            return Color.green;
        } 
        else if ((countR == 2 && countB == 1) || (countB == 2 && countR == 1))
        {
            GlobalManager.Instance.CurrentColorType = ColorType.Purple;
            return new Color(1, 0, 1, 1); //Purple
        }
        else if (countR == 1 && countY == 1 && countB == 1)
        {
            GlobalManager.Instance.CurrentColorType = ColorType.White;
            return Color.white;
        } 

        return Color.black; //Check how else can we make Black?
    }
}
