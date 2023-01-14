using System.Collections.Generic;
using System.Linq;
using Player;
using Statics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Building
{
    public class Builder : MonoBehaviour
    {
        public static int SelectedBuilding { get; set; } = 0;
        
        [SerializeField, Header("Tiles")]
        private List<AnimatedTile> buildings;
        [SerializeField]
        private List<Tile> gizmo;
        
        [SerializeField]
        private Tile placeholder;
        
        [SerializeField, Header("Target Grid and Tilemaps")]
        private Grid grid;
        
        [SerializeField]
        private List<Tilemap> tilemaps;

        [SerializeField, Header("Audio")]
        private AudioSource source; 
        public AudioClip buildSound;

        private bool buildingState;
        private bool everyTileIsValid;
        
        void OnEnable()
        {
            EventManager.PlayerClick += OnPlayerClickBuild;
            EventManager.EnableBuildMode += OnEnableBuildMode;
        }

        void Update()
        {
            if (buildingState) { WhileOnBuildModeDrawGizmoAndCheckAllTilesValidity(); }
        }
        

        private void OnEnableBuildMode()
        {
            buildingState = !buildingState;
            tilemaps[^1].ClearAllTiles();
        }
        
        private void WhileOnBuildModeDrawGizmoAndCheckAllTilesValidity()
        {
            if (buildingState)
            {
                Vector3Int? cell = CheckTileValidityToBuild(MainController.GetWorldMousePosition(), true);
                
                if (cell != null)
                {
                    everyTileIsValid = true;
                    tilemaps[^1].ClearAllTiles();
                    
                    Vector3Int cellConverted = (Vector3Int)cell;
                    
                    tilemaps[^1].SetTile(cellConverted, gizmo[0]);

                    List<Vector3Int> topNeighbors = Utils.BuildingTopNeighbors(cellConverted);

                    foreach (Vector3Int neighbor in topNeighbors)
                    {
                        if (CheckTileValidityToBuild(neighbor, false) != null)
                        {
                            tilemaps[^1].SetTile(neighbor, gizmo[0]);
                        }
                        else
                        {
                            tilemaps[^1].SetTile(neighbor, gizmo[1]);
                            everyTileIsValid = false;
                        }
                    }
                }
                else
                {
                    tilemaps[^1].ClearAllTiles();
                    Vector3Int invalidCell = (Vector3Int)Utils.WorldToCell(grid, MainController.GetWorldMousePosition());
                    invalidCell.z = 2;
                    tilemaps[^1].SetTile(invalidCell, gizmo[1]);
                }
     
            }
        }
        
        private void OnPlayerClickBuild(Vector3 mousePosition)
        {
            if (buildingState && everyTileIsValid)
            {
                Vector3Int? cell = CheckTileValidityToBuild(mousePosition, true);

                if (cell != null)
                {
                    tilemaps[1].SetTile((Vector3Int)cell, buildings[SelectedBuilding]);
                    Vector3Int cellConverted = (Vector3Int)cell;
                    
                    List<Vector3Int> topNeighbors = Utils.BuildingTopNeighbors(cellConverted);

                                      
                    List<Vector3Int> allNeighborsInRangeAndHeight =
                        Utils.GetAllNeighborsInRangeAndHeight(cellConverted, 2, 2);
                    
                    foreach (Vector3Int neighbor in topNeighbors)
                    {
                        tilemaps[1].SetTile(neighbor, placeholder);
                        allNeighborsInRangeAndHeight.AddRange(Utils.GetAllNeighborsInRangeAndHeight(neighbor, 2, 2));
                    }

                    allNeighborsInRangeAndHeight =  allNeighborsInRangeAndHeight.Distinct().ToList();
                    
                    foreach (Vector3Int neighbor in allNeighborsInRangeAndHeight)
                    {
                        tilemaps[3].SetTile(neighbor, null);
                    }
                    
                    tilemaps[3].SetTile(new Vector3Int(cellConverted.x, cellConverted.y, 2), null);
                    
                    source.PlayOneShot(buildSound, 0.25f);
                    buildingState = false;
                    tilemaps[^1].ClearAllTiles();
                }
            }
        }

        private Vector3Int? CheckTileValidityToBuild(Vector3 targetPosition, bool convertToCell)
        {

            Vector2Int cell = convertToCell ? Utils.WorldToCell(grid, targetPosition) : new Vector2Int((int)targetPosition.x, (int)targetPosition.y);
            
            int x = cell.x;
            int y = cell.y;

            if (tilemaps[0].GetTile(new Vector3Int(x, y, 0)) != null) { return null; }

            if  (
                   tilemaps[2].GetTile(new Vector3Int(x, y, 3)) != null 
                || tilemaps[1].GetTile(new Vector3Int(x, y, 1)) == null 
                || tilemaps[1].GetTile(new Vector3Int(x, y, 2)) != null
                ) 
            { return null; }
            
            return new Vector3Int(x, y, 2);
        }

        
        public List<AnimatedTile> GetBuildings()
        {
            return buildings;
        }
    }
}