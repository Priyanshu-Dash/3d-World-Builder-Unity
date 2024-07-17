using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerCamera;

    public void ToggleCamera()
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        playerCamera.SetActive(!playerCamera.activeSelf);
    }
}
