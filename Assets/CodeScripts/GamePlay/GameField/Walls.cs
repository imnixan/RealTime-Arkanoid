using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Walls : MonoBehaviour
{
    public event UnityAction MeteoritEnter;
    public event UnityAction BulletBounced;

    private void Awake()
    {
        AddWallsCollider();
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.gravityScale = 0;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteorit"))
        {
            MeteoritEnter?.Invoke();
            collision.GetComponent<Meteorit>().Explode(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletBounced?.Invoke();
    }
}
