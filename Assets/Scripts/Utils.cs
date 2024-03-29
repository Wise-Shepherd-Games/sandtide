using UnityEngine;
using System.Collections.Generic;

namespace Statics
{
	public static class Utils
	{
		public static Vector2Int WorldToCell(Grid grid, Vector3 positionInWorld)
		{
			Vector3 toCell = grid.WorldToCell(new Vector3(positionInWorld.x, positionInWorld.y - 0.26f, 0));
			Vector2Int toCellConverted = new Vector2Int((int)toCell.x, (int)toCell.y);
			return toCellConverted;
		}
	}
}
