using UnityEngine;
using System.Collections.Generic;

namespace Statics
{
	public static class Utils
	{
		public static Vector2Int WorldToCell(Grid grid, Vector3 positionInWorld)
		{
			Vector3 toCell = grid.WorldToCell(new Vector3(positionInWorld.x, positionInWorld.y - 0.26f, positionInWorld.z));
			Vector2Int toCellConverted = new Vector2Int((int)toCell.x, (int)toCell.y);
			return toCellConverted;
		}

		public static List<Vector3Int> BuildingTopNeighbors(Vector3Int positionInGrid)
		{
			List<Vector3Int> topNeighbors = new List<Vector3Int>();
			topNeighbors.Add(new Vector3Int(positionInGrid.x + 1, positionInGrid.y + 1, positionInGrid.z));
			topNeighbors.Add(new Vector3Int(positionInGrid.x + 1, positionInGrid.y, positionInGrid.z));
			topNeighbors.Add(new Vector3Int(positionInGrid.x, positionInGrid.y + 1, positionInGrid.z));
			return topNeighbors;
		}

		public static List<Vector3Int> GetAllNeighborsInRangeAndHeight(Vector3Int positionInGrid, int height = 0, int range = 2)
		{
			Vector3Int cell = positionInGrid;
			List<Vector3Int> allNeighbors = new List<Vector3Int>();
			
			for (int i = 0; i < range; i++)
			{
				for (int j = 0; j < range; j++)
				{
					allNeighbors.Add(new Vector3Int(cell.x + i, cell.y + j, height));
					allNeighbors.Add(new Vector3Int(cell.x - i, cell.y - j, height));
					allNeighbors.Add(new Vector3Int(cell.x + i, cell.y - j, height));
					allNeighbors.Add(new Vector3Int(cell.x - i, cell.y + j, height));
				}
			}

			return allNeighbors;
		}
	}
}
