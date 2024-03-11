using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerSpawner : MonoBehaviour
{

    [Header("Settings:")]
    [SerializeField] LayerMask groundLayers;
    private Tower towerIndicator;

    [Header("Towers user can buy:")]
    // Types of towers
    [SerializeField] Tower defaultTower;
    // more to come...

    // grid related
    [Header("Grid:")]
    [SerializeField] Tilemap tilemap;
    private Vector3Int cellPosition;


    private bool spawnerIsActive;

    private void Awake()
    {
        // Intial setup
        spawnerIsActive = false;
        towerIndicator = null;
    }

    private void Update()
    {

        // create a tower with a button press
        if (Input.GetKeyUp(KeyCode.T) && !spawnerIsActive)
        {
            towerIndicator = Instantiate(defaultTower, GetMousePosition(), Quaternion.identity);
            spawnerIsActive = true;
        }

        // place the tower
        if (spawnerIsActive)
        {
            towerIndicator.transform.position = GetMousePosition();

            // drop the tower
            if (Input.GetMouseButton(0))
            {
                towerIndicator.activateTower();
                towerIndicator = null;
                spawnerIsActive = false;
            }

            // cancel placement
            else if (Input.GetMouseButton(1))
            {
                Destroy(towerIndicator.gameObject);
                spawnerIsActive = false;
            }


        }



        towerIndicator.transform.position = GetMousePosition();
    }


    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Fire the 'laser' 
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 200f, groundLayers))
        {

            // We hit something! Yippee!
            Debug.DrawLine(Camera.main.transform.position, hit.point,
                Color.red);

            // convert hit point to a position on the grid
            cellPosition = tilemap.LocalToCell(hit.point);

            return new Vector3(
                cellPosition.x + tilemap.cellSize.x / 2f,    // x
                0,                                           // y
                cellPosition.y + tilemap.cellSize.y / 2f    // z
               );

        }

        return Vector3.zero;

    }




}
