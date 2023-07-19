using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BgScaler : MonoBehaviour
{
    private void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Image image = GetComponent<Image>();
        image.SetNativeSize();
        Vector2 screenSize = transform.parent.GetComponent<RectTransform>().rect.size;
        Vector2 newBgSize = rt.sizeDelta;
        float scale = newBgSize.y / screenSize.y;
        newBgSize /= scale;
        rt.sizeDelta = newBgSize;
    }
}
