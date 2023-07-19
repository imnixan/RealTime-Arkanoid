using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Meteorit : MonoBehaviour
{
    public static event UnityAction<bool, Vector2> MeteoritDestroyed;
    private Rigidbody2D rb;

    public void Init(Sprite sprite)
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        Image image = GetComponent<Image>();
        image.sprite = sprite;
        image.SetNativeSize();
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.isKinematic = true;
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.radius = GetComponent<RectTransform>().sizeDelta.x / 2;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(
            Vector2.MoveTowards(
                transform.position,
                (Vector2)transform.position - Vector2.up,
                Time.fixedDeltaTime * GlobalStat.GameSpeed
            )
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode(true);
    }

    public void Explode(bool playerKill)
    {
        MeteoritDestroyed?.Invoke(playerKill, transform.position);
        Destroy(gameObject);
    }
}
