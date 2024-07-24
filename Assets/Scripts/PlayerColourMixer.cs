using System.Collections;
using System.Collections.Generic;
using Fontana = System.Drawing;
//using UnityEditor.Search;
using UnityEngine;

public class PlayerColourMixer : MonoBehaviour
{
    public static Queue<Fontana.Color> mainPalette;

    void Start()
    {
        mainPalette = new Queue<Fontana.Color>();

        //Adding colors into Queue and into GlobalManager
        AddColor(Fontana.Color.Red, mainPalette);
        AddColor(Fontana.Color.Yellow, mainPalette);
        AddColor(Fontana.Color.Blue, mainPalette);

        GlobalManager.Instance.CurrentPaletteColor1 = Fontana.Color.Red;
        GlobalManager.Instance.CurrentPaletteColor2 = Fontana.Color.Yellow;
        GlobalManager.Instance.CurrentPaletteColor3 = Fontana.Color.Blue;
        GlobalManager.Instance.CurrentPaletteColorResult = Fontana.Color.White;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddColor(Fontana.Color.Red, mainPalette);
            StorePaletteColors(mainPalette);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddColor(Fontana.Color.Yellow, mainPalette);
            StorePaletteColors(mainPalette);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddColor(Fontana.Color.Blue, mainPalette);
            StorePaletteColors(mainPalette);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Fontana.Color resultColor = DetermineColor(mainPalette);

            //Rework if needed
            GlobalManager.Instance.CurrentPaletteColorResult = resultColor;
        }
    }

    private void AddColor(Fontana.Color paletteColor, Queue<Fontana.Color> paletteToAdd)
    {
        if (paletteToAdd.Count == 3) //3 is the maximum size
        {
            paletteToAdd.Dequeue();
        }
        paletteToAdd.Enqueue(paletteColor);
    }

    private void StorePaletteColors(Queue<Fontana.Color> paletteToStore)
    {
        Fontana.Color[] paletteArray = paletteToStore.ToArray(); //Dont like how I made this, rework later

        GlobalManager.Instance.CurrentPaletteColor1 = paletteArray[0];
        GlobalManager.Instance.CurrentPaletteColor2 = paletteArray[1];
        GlobalManager.Instance.CurrentPaletteColor3 = paletteArray[2];
    }

    private Fontana.Color DetermineColor(Queue<Fontana.Color> palette)
    {   
        int countR = 0;
        int countY = 0;
        int countB = 0;

        foreach (Fontana.Color color in palette)
        {
            if (color == Fontana.Color.Red) countR++;
            else if (color == Fontana.Color.Yellow) countY++;
            else if (color == Fontana.Color.Blue) countB++;
        }

        if (countR == 3) return Fontana.Color.Red; //This wont work the first time when there are less than 3 colors in palette
        if (countY == 3) return Fontana.Color.Yellow;
        if (countB == 3) return Fontana.Color.Blue;
        if ((countR == 2 && countY == 1) || (countY == 2 && countR == 1)) return Fontana.Color.DarkOrange;
        if ((countY == 2 && countB == 1) || (countB == 2 && countY == 1)) return Fontana.Color.Green;
        if ((countR == 2 && countB == 1) || (countB == 2 && countR == 1)) return Fontana.Color.Indigo;
        if (countR == 1 && countY == 1 && countB == 1) return Fontana.Color.White;

        return Fontana.Color.Black; //Check how else can we make Black
    }
}
