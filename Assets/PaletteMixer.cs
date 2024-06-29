using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fontana = System.Drawing;

public class PaletteMixer : MonoBehaviour
{
    public Image UIPalleteColor1;
    public Image UIPalleteColor2;
    public Image UIPalleteColor3;
    public Image UIPalleteColorResult;

    private Fontana.Color color1;
    private Fontana.Color color2;
    private Fontana.Color color3;
    private Fontana.Color colorResult;

    // Start is called before the first frame update
    void Start()
    {
        RetrieveColors();
    }

    // Update is called once per frame
    void Update()
    {
        RetrieveColors();

        UIPalleteColor1.color = new UnityEngine.Color(color1.R, color1.G, color1.B, color1.A);
        UIPalleteColor2.color = new UnityEngine.Color(color2.R, color2.G, color2.B, color2.A);
        UIPalleteColor3.color = new UnityEngine.Color(color3.R, color3.G, color3.B, color3.A);
        UIPalleteColorResult.color = new UnityEngine.Color(colorResult.R, colorResult.G, colorResult.B, colorResult.A);

    }

    private void RetrieveColors()
    {
        color1 = GlobalManager.Instance.CurrentPaletteColor1;
        color2 = GlobalManager.Instance.CurrentPaletteColor2;
        color3 = GlobalManager.Instance.CurrentPaletteColor3;
        colorResult = GlobalManager.Instance.CurrentPaletteColorResult;
    }
}
