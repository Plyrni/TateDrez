using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTouchManager : MonoBehaviour
{
    public static TileTouchManager Instance { get => instance; set => instance = value; }
    private static TileTouchManager instance;

    [SerializeField] LayerMask _layerMaskTile;

    private Camera _camera; // Initialized at game start. Used to keep the reference and avoid unecessary long calling lines "CameraManager.Instance.mainCamera"

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this._camera = CameraManager.Instance.mainCamera;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.OnInputTouch();
        }
    }


    private void OnInputTouch()
    {
        ITileInterractable interractableTile = this.TryRaycastTileInterractable();
        if (interractableTile != null)
        {
            interractableTile.OnTouch();
        }
    }

    private ITileInterractable TryRaycastTileInterractable()
    {
        // Raycast from cam to world. Trying to collide with a tile
        Ray rayCam = this._camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(rayCam, out hit, 40, this._layerMaskTile) == false)
        {
            return null; // Return if no collision
        }

        // On collision detection, try to get ITileInterractable component
        ITileInterractable tempTile = null;
        Rigidbody tileRB = hit.collider.attachedRigidbody;
        if (tileRB != null)
        {
            tempTile = tileRB.GetComponent<ITileInterractable>();
        }
        return tempTile;
    }
}
