using UnityEngine;
using UnityEngine.UI;

public class GunLaserSight : MonoBehaviour
{
    private bool turnedOn;
    private LineRenderer lineRenderer;
    private Vector2 endPoint;
    private RaycastHit2D hit;
    private RectTransform gunRt;
    private Vector3[] gunCorners;
    public Vector2 leftCorner,
        rightCorner;
    private Gun gun;
    private GunSight gunSight;
    private GameObject arrow;
    private Image arrowImage;

    public void Init(RectTransform gunRt)
    {
        this.gunRt = gunRt;
        gun = gunRt.GetComponent<Gun>();
        gunSight = gunRt.GetComponentInChildren<GunSight>();
        InitLineRenderer();
        gunCorners = new Vector3[4];
        InitArrow();
    }

    private void InitLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = lineRenderer.startWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.material = Resources.Load<Material>("LineRendererMat");
        lineRenderer.textureMode = LineTextureMode.Tile;
        lineRenderer.sortingOrder = 6;
    }

    private void InitArrow()
    {
        arrow = Instantiate(new GameObject("arrow"), transform);
        arrowImage = arrow.AddComponent<Image>();
        arrowImage.sprite = Resources.Load<Sprite>("SightArrow");
        arrowImage.raycastTarget = false;
        arrowImage.SetNativeSize();
        RectTransform arrowRect = arrow.GetComponent<RectTransform>();
        arrowRect.pivot = new Vector2(0.5f, 1);
        arrowRect.sizeDelta = arrowRect.sizeDelta / 2;
    }

    public void SetLaserStatus(bool turnedOn)
    {
        this.turnedOn = turnedOn;
    }

    private void LateUpdate()
    {
        lineRenderer.enabled = turnedOn;
        arrowImage.enabled = turnedOn;
        if (turnedOn)
        {
            SetupLine();
            SetupArrow();
        }
    }

    private void SetupLine()
    {
        lineRenderer.startColor = gun.CanShootByTimer ? Color.white : Color.gray;
        lineRenderer.endColor = lineRenderer.startColor;
        lineRenderer.SetPosition(0, GetStartPosition());
        lineRenderer.SetPosition(1, GetEndPoint());
    }

    private void SetupArrow()
    {
        arrow.transform.position = GetEndPoint();
        arrowImage.color = gun.CanShootByTimer ? Color.white : Color.gray;
    }

    public Vector3 GetEndPoint()
    {
        hit = Physics2D.Raycast(
            GetStartPosition(),
            gunRt.up,
            Mathf.Infinity,
            LayerMask.GetMask("Raycastable")
        );
        return hit.point;
    }

    public Vector2 GetStartPosition()
    {
        Vector2 startPosition = new Vector2();
        gunRt.GetWorldCorners(gunCorners);
        leftCorner = gunCorners[1];
        rightCorner = gunCorners[2];
        startPosition.x = gunCorners[1].x + (gunCorners[2].x - gunCorners[1].x) / 2;
        startPosition.y = gunCorners[1].y + (gunCorners[2].y - gunCorners[1].y) / 2;
        return startPosition;
    }
}
