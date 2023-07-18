using System.Collections;
using UnityEngine;

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

    public void Init(RectTransform gunRt)
    {
        this.gunRt = gunRt;
        gun = gunRt.GetComponent<Gun>();
        gunSight = gunRt.GetComponentInChildren<GunSight>();
        InitLineRenderer();
        gunCorners = new Vector3[4];
    }

    private void InitLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = lineRenderer.startWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.material = Resources.Load<Material>("LineRendererMat");
        lineRenderer.textureMode = LineTextureMode.Tile;
    }

    public void SetLaserStatus(bool turnedOn)
    {
        this.turnedOn = turnedOn;
    }

    private void LateUpdate()
    {
        lineRenderer.enabled = turnedOn;
        if (turnedOn)
        {
            SetupLine();
        }
    }

    private void SetupLine()
    {
        lineRenderer.startColor = gun.CanShootByTimer ? Color.white : Color.gray;
        lineRenderer.endColor = lineRenderer.startColor;
        lineRenderer.SetPosition(0, GetStartPosition());
        lineRenderer.SetPosition(1, GetEndPoint());
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
