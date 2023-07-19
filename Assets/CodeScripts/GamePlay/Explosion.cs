using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Explosion : MonoBehaviour
{
    [SerializeReference]
    private Sprite[] explosionFrames;

    private const float ExplosionSpeed = 0.05f;

    private void Start()
    {
        StartCoroutine(ExplosionAnimation());
    }

    private IEnumerator ExplosionAnimation()
    {
        Image currentFrame = GetComponent<Image>();
        int animationLenght = explosionFrames.Length;

        for (int i = 1; i < animationLenght; i++)
        {
            yield return new WaitForSeconds(ExplosionSpeed);
            currentFrame.sprite = explosionFrames[i];
            currentFrame.color += new Color(0, 0, 0, -0.25f);
        }
        Destroy(gameObject);
    }
}
