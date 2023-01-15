using System;
using Statics;
using System.Collections.Generic;
using UnityEngine;
using EventManagers;

namespace Construction
{
	public class ConstructionBase : MonoBehaviour
	{
		public Enums.ConstructionTypes constructionType;
    
		[Min(0)]
		public int maxLevel, currentLevel, warehouseFilled, warehouseCapacity;
 
		[Min(0)]
		public float activeNoise, inactiveNoise, viewRadius, overallEfficiency;

		public List<Vector3Int> cellsAllocatedOnGrid;

		public bool isSelected;

		public SpriteRenderer spriteRenderer;

		public Animator animator;

		public CircleCollider2D circleCollider;

		public void EmitNoise(float noiseCalculated) => NoiseEventManager.OnMakeNoise(gameObject, noiseCalculated);

		public void Destroy()
		{
        
		}

		public void OnMouseDown()
		{
			
		}

		public void OnMouseOver()
		{
        
		}
	}
}
