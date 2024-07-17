using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;
    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayermask;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        // if the player clicks left mouse button, invoke the OnClicked event
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();

        // if the player presses escape, invoke the OnExit event
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    // check if the mouse is over a UI element
    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition()
    {
        // get the pos of the mouse or touch (works with mobile)
        Vector3 mousePos = Input.mousePosition;

        // so that only points in the camera view are selectable
        mousePos.z = sceneCamera.nearClipPlane;

        // shoot a ray from the camera to the mouse position
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, placementLayermask))
        {
            // if the ray hits ground (as it is the only thing in the scene), return the position
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}