using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    private int selectedObject = -1;

    private void Start()
    {
        StopPlacement();
    }

    private void Update()
    {
        // if no object is selected (from the UI), do nothing
        if (selectedObject < 0)
        {
            return;
        }

        // get the current mouse position and move the indicator to it
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;

        // discrete cells, so int
        // convert mouse world pos to game cell pos
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // move the cell indicator to the cell's world position
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

    public void StartPlacement(int ID)
    {
        // stop placement of previous object before starting a new one
        StopPlacement();

        // find the index of the selected object in the SO DB
        // this is shorthand for a for loop
        selectedObject = database.objectsData.FindIndex(data => data.ID == ID);

        if (selectedObject < 0)
        {
            Debug.LogError("Not found");
            return;
        }

        // subscribe to the events defined in InputManager.cs
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void PlaceStructure()
    {
        // if the mouse is over the UI, do nothing
        if (inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();

        // get the cell position of the mouse position
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // instantiate the object at the cell position from the SO DB
        GameObject newObject = Instantiate(database.objectsData[selectedObject].Prefab);
        audioSource.PlayOneShot(audioClip);

        // set the object's position to the cell's world position
        newObject.transform.position = grid.CellToWorld(gridPosition);
    }

    public void StopPlacement()
    {
        selectedObject = -1;
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }
}
