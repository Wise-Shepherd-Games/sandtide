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
	}
}
