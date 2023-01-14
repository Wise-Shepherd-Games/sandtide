using Statics;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

namespace World
{
    public class EnvironmentBehaviour : MonoBehaviour
    {
        [SerializeField, Header("Target Grid")]
        private Grid grid;

        
        [SerializeField, Header("Target Tilemap")]
        private Tilemap highGround;

        private List<Vector3Int> tilesChanged = new List<Vector3Int>();
        
        void Update()
        {
            OnMouseOverHighGround();
        }

        private void OnMouseOverHighGround()
        {
            Vector2Int cell = Utils.WorldToCell(grid, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            List<Vector2Int> neighbors = new List<Vector2Int>();
            List<Vector3Int> tilesForTransparency = new List<Vector3Int>();
            
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    neighbors.Add(new Vector2Int(cell.x + i, cell.y + j ));
                    neighbors.Add(new Vector2Int(cell.x - i, cell.y - j));
                    neighbors.Add(new Vector2Int(cell.x + i, cell.y - j ));
                    neighbors.Add(new Vector2Int(cell.x - i, cell.y + j));
                }
            }
            
            neighbors = neighbors.Distinct().ToList();

            for (int i = 0; i < neighbors.Count; i++)
            {
                int z = 3;
                if (highGround.GetTile(new Vector3Int(neighbors[i].x, neighbors[i].y, z)) != null)
                {
                    while (highGround.GetTile(new Vector3Int(neighbors[i].x, neighbors[i].y, z)) != null)
                    {
                        tilesForTransparency.Add(new Vector3Int(neighbors[i].x, neighbors[i].y, z));
                        z += 2;
                    }
                }
            }
            
            if (tilesForTransparency.Count > 0)
            {
                foreach (Vector3Int tile in tilesForTransparency)
                {
                    if(!tilesChanged.Contains(tile)) tilesChanged.Add(tile);
                    highGround.SetTileFlags(tile, TileFlags.None);
                    highGround.SetColor(tile, new Color(){r = 255f, b = 255f, g = 255f, a =  0.25f});
                }

            }
            else
            {
                foreach (Vector3Int tile in tilesChanged)
                {
                    highGround.SetTileFlags(tile, TileFlags.None);
                    highGround.SetColor(tile, new Color(){r = 255f, b = 255f, g = 255f, a =  1f});
                }

                tilesChanged.Clear();
            }
            
        }
    }
}