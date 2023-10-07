using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-1)]
public class TileTouchManager : MonoBehaviour
{
    public static TileTouchManager Instance { get => instance; set => instance = value; }
    private static TileTouchManager instance;
    [SerializeField] LayerMask _layerMaskTile;
    private Camera _camera; // Initialized at game start. Used to keep the reference and avoid unecessary long calling lines "CameraManager.Instance.mainCamera"

    public UnityEvent<ChessTile2D> OnTileTouched;

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
        ITouchInterractable interractable = this.TryRaycastTileInterractable();
        if (interractable != null)
        {
            interractable.OnTouch();

            ChessTile2D tile = interractable as ChessTile2D;
            if (tile != null)
            {
                this.OnTileTouched?.Invoke(tile);
            }
        }
    }
    private ITouchInterractable TryRaycastTileInterractable()
    {
        // Raycast from cam to world. Trying to collide with a tile
        Ray rayCam = this._camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(rayCam, out hit, 40, this._layerMaskTile) == false)
        {
            return null; // Return if no collision
        }

        // On collision detection, try to get ITileInterractable component
        ITouchInterractable tempTile = null;
        Rigidbody tileRB = hit.collider.attachedRigidbody;
        if (tileRB != null)
        {
            tempTile = tileRB.GetComponent<ITouchInterractable>();
        }
        return tempTile;
    }
}
