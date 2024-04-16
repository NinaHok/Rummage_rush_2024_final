using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{

    [Header("Settings:")]
    [SerializeField] LayerMask groundLayers;
    private Tower towerIndicator;

    [Header("Towers user can buy:")]
    // Types of towers
    [SerializeField] Tower towerDefault;
    [SerializeField] Tower towerHeavy;
    [SerializeField] Tower towerFast;

    private Tower towerMarker;

    // grid related
    [Header("Grid:")]
    [SerializeField] Tilemap tilemap;
    private Vector3Int cellPosition;


    private bool spawnerIsActive;

    private Vector3 mousePosition;

    [SerializeField] GameSettingsSO gameSettings;
    [SerializeField] HUDmanager hudManager;

    //buttons
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;

    private void Awake()
    {
        // Intial setup
        spawnerIsActive = false;
        towerIndicator = null;
    }

    private void Start()
    {
        button1.onClick.AddListener(delegate { StartTowerPlacement(towerDefault); });
        button2.onClick.AddListener(delegate { StartTowerPlacement(towerFast); });
        button3.onClick.AddListener(delegate { StartTowerPlacement(towerHeavy); });
    }

    private void Update()
    {

        // create a tower with a button press
        if (Input.GetKeyUp(KeyCode.Alpha1) && !spawnerIsActive && gameSettings.money > 0)
        {
            StartTowerPlacement(towerDefault);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) && !spawnerIsActive && gameSettings.money > 0)
        {
            StartTowerPlacement(towerFast);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3) && !spawnerIsActive && gameSettings.money > 0)
        {
            StartTowerPlacement(towerHeavy);
        }

            if (Input.GetKeyUp(KeyCode.Alpha1) && !spawnerIsActive && gameSettings.money == 0)
        {
            Debug.Log($"Not enough money!");
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

    private void StartTowerPlacement(Tower newTower)
    {
        if (!spawnerIsActive && gameSettings.money > 0)
        {
            towerIndicator = Instantiate(newTower, mousePosition, Quaternion.identity);
            spawnerIsActive = true;
            hudManager.UpdateMoneyText();
        }

        else
        {
            Debug.Log($"Not enough money!");
        }
           
    } 


}
