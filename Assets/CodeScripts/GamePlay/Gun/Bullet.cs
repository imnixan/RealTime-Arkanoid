using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float BulletSpeed = 3;
    private const int MaxBounces = 5;
    private Rigidbody2D rb;
    private int bouncesLeft;

    private void Init()
    {
        RectTransform rt = GetComponent<RectTransform>();
        GetComponentInChildren<TrailRenderer>().startWidth = GetGlobalWidth(rt);
        bouncesLeft = MaxBounces;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<CircleCollider2D>().radius = rt.sizeDelta.x / 2;
    }

    public void BulletShot(Vector3 destination)
    {
        //transform.position = destination;

        if (rb == null)
        {
            Init();
        }
        rb.velocity = (destination - transform.position) * BulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bouncesLeft--;
        if (bouncesLeft == 0)
        {
            Destroy(gameObject);
        }
    }

    private float GetGlobalWidth(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return corners[2].x - corners[1].x;
    }
}
