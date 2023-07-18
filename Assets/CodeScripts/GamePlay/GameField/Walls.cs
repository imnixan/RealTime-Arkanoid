using System.Collections;
using UnityEngine;

public class Walls : MonoBehaviour
{
    private void Awake()
    {
        AddWallsCollider();
    }

    private void AddWallsCollider()
    {
        foreach (Transform wall in transform)
        {
            BoxCollider2D boxCollider2D = wall.gameObject.AddComponent<BoxCollider2D>();
            RectTransform wallRt = wall.GetComponent<RectTransform>();
            boxCollider2D.size = wallRt.rect.size;
        }
    }
}
