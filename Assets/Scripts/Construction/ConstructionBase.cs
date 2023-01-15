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
		public GameObject selectedGizmo;
		private SpriteRenderer gizmo;

		public SpriteRenderer spriteRenderer;

		public Animator animator;

		public CircleCollider2D circleCollider;

		private void Awake()
		{
			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			
		}

		private void Start()
		{
			gizmo = selectedGizmo.GetComponent<SpriteRenderer>();
			PlayerInputEventManager.MouseLeftClick += CheckIfStillSelected;
		}

		public void EmitNoise(float noiseCalculated)
		{
			Debug.Log("Made noise: " + noiseCalculated);
			NoiseEventManager.OnMakeNoise(gameObject, noiseCalculated);
		}

		public void Destroy()
		{
        
		}

		private void CheckIfStillSelected(GameObject gameObj)
		{
				if (gameObj.transform.root != gameObject.transform)
				{
					isSelected = false;
					gizmo.enabled = false;
				}
		}
		
		public void OnMouseDown()
		{
			isSelected = !isSelected;

			if (isSelected)
			{
				gizmo.color  = new Color(255, 255, 255, 1f);
				gizmo.enabled = true;
			}
			else
			{
				gizmo.enabled = false;
			}
		}

		public void OnMouseEnter()
		{
			if (isSelected == false)
			{
				gizmo.color = new Color(255, 255, 255, 0.01f);
				gizmo.enabled = true;
			}
		}

		public void OnMouseOver()
		{
			if (isSelected == false)
			{
				Color color = gizmo.color;
			
				if(color.a < 0.7)
					color = new Color(255, 255, 255, color.a + 0.03f);
			
				gizmo.color = color;
			}
		}

		public void OnMouseExit()
		{
			if (isSelected == false)
			{
				gizmo.color = new Color(255, 255, 255, 0.01f);
				gizmo.enabled = false;
			}
		}
		
		
	}
}
