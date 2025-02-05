
using UnityEngine;

public class ColorManager : LoadAuto
{
    public void RandomColor(GameObject obj, int param)
    {
        obj.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.HSVToRGB((param / 100f) % 1f, 1f, 1f));
    }
}
