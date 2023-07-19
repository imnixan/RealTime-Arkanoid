using System.Collections;
using UnityEngine;

public class MeteoritsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject meteoritPrefab;

    [SerializeField]
    private Sprite[] meteoritsSprite;
    private float spawnsPerSeconds;
    private RectTransform gameField;
    private Vector3[] fieldCorners;
    private float meteoritWindowWidth;
    private int meteoritsMaxWidthCount;
    private float Timer
    {
        get { return 1f / spawnsPerSeconds; }
    }

    private void Start()
    {
        Physics.IgnoreLayerCollision(7, 7, true);

        RectTransform rt = GetComponent<RectTransform>();

        fieldCorners = new Vector3[4];
        rt.GetWorldCorners(fieldCorners);

        meteoritsMaxWidthCount = (int)(rt.rect.width / 120);
        meteoritWindowWidth = fieldCorners[2].x * 2 / meteoritsMaxWidthCount;

        StartCoroutine(SpawnAsteroid());
    }

    private IEnumerator SpawnAsteroid()
    {
        while (true)
        {
            for (
                int xPos = (meteoritsMaxWidthCount / -2) + 1;
                xPos < meteoritsMaxWidthCount / 2;
                xPos++
            )
            {
                spawnsPerSeconds = GlobalStat.GameSpeed * 0.6f;
                int chanse = Random.Range(0, 10);
                if (chanse > 7)
                {
                    Instantiate(
                            meteoritPrefab,
                            new Vector2(
                                xPos * meteoritWindowWidth,
                                fieldCorners[1].y + 1 + Random.Range(-0.3f, 0.3f)
                            ),
                            new Quaternion(),
                            transform
                        )
                        .GetComponent<Meteorit>()
                        .Init(meteoritsSprite[Random.Range(0, meteoritsSprite.Length)]);
                }
            }
            yield return new WaitForSecondsRealtime(Timer);
        }
    }
}
