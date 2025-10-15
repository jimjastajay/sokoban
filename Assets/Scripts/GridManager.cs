using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GridManager : MonoBehaviour
{

    public float cellSize;
    public Vector2 gridSize;
    public List<List<GameObject>> gridList = new List<List<GameObject>>();

    [SerializeField]
    private GameObject canvas, label, emptyCell, gridBounds;

    [SerializeField]
    private float debugY, raycastCheckY;

    [SerializeField]
    private bool debugMode;

    private void Awake()
    {
        MakeGrid();
    }

    /// <summary>
    /// Makes grid data structure based on level placement in the scene
    /// </summary>
    private void MakeGrid()
    {
        gridSize = new Vector2((int)gridBounds.transform.localScale.x / cellSize, (int)gridBounds.transform.localScale.z);
        Vector3 checkSpot = Vector3.zero;
        int startX = (int)(gridBounds.transform.position.x - gridBounds.transform.localScale.x / 2);
        int startY = (int)(gridBounds.transform.position.z + gridBounds.transform.localScale.z / 2);
        for (int x = 0; x < gridSize.x; x++)
        {
            gridList.Add(new List<GameObject>());
            for (int y = 0; y < gridSize.y; y++)
            {
                checkSpot.Set(startX + x, transform.position.y, startY - y);
                if (debugMode) AddDebugUI(checkSpot, x, y);
                GameObject newCell = Instantiate(emptyCell, checkSpot, Quaternion.identity);
                newCell.transform.SetParent(transform);
                gridList[x].Add(newCell);
                checkSpot.y = raycastCheckY;
                RaycastHit hit;
                if (Physics.SphereCast(checkSpot, 0.1f, transform.up, out hit, 1f))
                {
                    if (hit.collider.CompareTag("block") || hit.collider.CompareTag("Player"))
                    {
                        hit.collider.gameObject.GetComponent<Block>().SetupNewGridBlock(this, newCell, x, y);
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Includes grid coordinate text for each cell in the game view
    /// </summary>
    private void AddDebugUI(Vector3 _pos, int _gridX, int _gridY)
    {
        _pos.y = debugY;
        GameObject newLabel = Instantiate(label, _pos, Quaternion.identity, canvas.transform);
        newLabel.GetComponent<TMP_Text>().text = _gridX + "," + _gridY;
    }
}