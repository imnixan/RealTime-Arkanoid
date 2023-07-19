using UnityEngine;

public class GunSight : MonoBehaviour
{
    private Vector2 fingerPosition;
    private Camera camera;
    public bool _onTap;
    private Vector3[] parentCorners;
    private Gun gun;
    private Transform gunTransform;

    private bool OnTap
    {
        get { return _onTap; }
        set
        {
            if (_onTap != value)
            {
                _onTap = value;
                gun.UpdateSightStatus(_onTap);
            }
        }
    }

    public void Init(RectTransform gameField, Transform gunTransform)
    {
        this.gunTransform = gunTransform;
        gun = gunTransform.GetComponent<Gun>();

        parentCorners = new Vector3[4];
        gameField.GetWorldCorners(parentCorners);
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnTap = true;
        }
        if (Input.GetMouseButtonUp(0) && _onTap)
        {
            OnTap = false;
        }
    }

    private void FixedUpdate()
    {
        if (_onTap)
        {
            fingerPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            gunTransform.up = fingerPosition - (Vector2)transform.position;
        }
    }
}
