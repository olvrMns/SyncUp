using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ImageColorSync : MonoBehaviour
{
    private Image image;
    public ColorSync colorSync;
    private Button button;
    private ColorBlock colorBlock;

    void Start()
    {
        image = GetComponent<Image>();
        if (colorSync == null) colorSync = GameObject.Find("ColorSyncImage").GetComponent<ColorSync>();
        button = GetComponent<Button>();
        if (button != null) colorBlock = button.colors;
    }

    void Update()
    {
        image.color = colorSync.CurrentColor;
         colorBlock.normalColor = colorSync.CurrentColor;
    }
}
