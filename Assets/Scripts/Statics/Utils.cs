using UnityEngine;

public static class Utils
{
	public static Vector2Int WorldToCell(Grid grid, Vector3 positionInWorld)
	{
		Vector3 toCell = grid.WorldToCell(positionInWorld);
		Vector2Int toCellConverted = new Vector2Int((int)toCell.x, (int)toCell.y);
		return toCellConverted;
	}
}
