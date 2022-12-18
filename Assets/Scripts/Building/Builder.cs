using System.Collections.Generic;
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
        private List<Tile> buildings;
        [SerializeField]
        private Tile gizmo;
        
        [SerializeField, Header("Target Grid and Tilemaps")]
        private Grid grid;
        
        [SerializeField]
        private List<Tilemap> tilemaps;

        [SerializeField, Header("Audio")]
        private AudioSource source; 
        public AudioClip buildSound;

        private bool buildingState;
        
        void OnEnable()
        {
            EventManager.PlayerClick += OnPlayerClickBuild;
            EventManager.EnableBuildMode += OnEnableBuildMode;
        }

        void Update()
        {
            if (buildingState) { WhileOnBuildModeDrawGizmo(); }
        }
        

        private void OnEnableBuildMode()
        {
            buildingState = !buildingState;
            tilemaps[^1].ClearAllTiles();
        } 
        
        private void WhileOnBuildModeDrawGizmo()
        {
            if (buildingState)
            {
                Vector3Int? cell = CheckTileValidityToBuild(MainController.GetWorldMousePosition());
                
                if (cell != null)
                {
                    tilemaps[^1].ClearAllTiles();
                    tilemaps[^1].SetTile((Vector3Int)cell, gizmo);
                }
                else
                {
                    tilemaps[^1].ClearAllTiles();
                }
     
            }
        }
        
        private void OnPlayerClickBuild(Vector3 mousePosition)
        {
            if (buildingState)
            {
                Vector3Int? cell = CheckTileValidityToBuild(mousePosition);

                if (cell != null)
                {
                    tilemaps[1].SetTile((Vector3Int)cell, buildings[SelectedBuilding]);
                    source.PlayOneShot(buildSound, 0.25f);
                    buildingState = false;
                    tilemaps[^1].ClearAllTiles();
                    tilemaps[2].color = Color.white;
                }
            }
        }

        private Vector3Int? CheckTileValidityToBuild(Vector3 targetPosition)
        {
            Vector2Int cell = Utils.WorldToCell(grid, targetPosition);
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

        public List<Tile>  GetBuildings()
        {
            return buildings;
        }
    }
}