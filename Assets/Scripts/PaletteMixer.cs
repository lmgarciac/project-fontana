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

    private Color color1;
    private Color color2;
    private Color color3;
    private Color colorResult;

    // Start is called before the first frame update
    void Start()
    {
        RetrieveColors();
    }

    // Update is called once per frame
    void Update()
    {
        RetrieveColors();

        UIPalleteColor1.color = new Color(color1.r, color1.g, color1.b, color1.a);
        UIPalleteColor2.color = new Color(color2.r, color2.g, color2.b, color2.a);
        UIPalleteColor3.color = new Color(color3.r, color3.g, color3.b, color3.a);
        UIPalleteColorResult.color = new Color(colorResult.r, colorResult.g, colorResult.b, colorResult.a);

    }

    private void RetrieveColors()
    {
        color1 = GlobalManager.Instance.CurrentPaletteColor1;
        color2 = GlobalManager.Instance.CurrentPaletteColor2;
        color3 = GlobalManager.Instance.CurrentPaletteColor3;
        colorResult = GlobalManager.Instance.CurrentPaletteColorResult;
    }
}
