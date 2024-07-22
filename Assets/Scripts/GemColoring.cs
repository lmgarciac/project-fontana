using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemColoring : MonoBehaviour
{
    private Renderer gemRenderer;

    private void Start()
    {
        gemRenderer = this.GetComponent<Renderer>();
    }

    void Update() //Maybe take this out of update into an event would be better.
    {
        gemRenderer.material.SetColor("_Color", new Color(GlobalManager.Instance.CurrentPaletteColorResult.R, GlobalManager.Instance.CurrentPaletteColorResult.G, GlobalManager.Instance.CurrentPaletteColorResult.B));
    }
}
