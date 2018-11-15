using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityEditor
{
	[CreateAssetMenu]
	[CustomGridBrush(false, true, false, "Tile Brush")]
	public class TileBrush : GridBrushBase
	{
		private const float k_PerlinOffset = 100000f;

		//public GameObject[] m_Prefabs;
		public GameObject topLeft;
		public GameObject topMiddle;
		public GameObject topRight;
		public GameObject middleLeft;
		public GameObject middle;
		public GameObject middleRight;
		public GameObject bottomLeft;
		public GameObject bottomMiddle;
		public GameObject bottomRight;

		public float m_PerlinScale = 0.5f;
		public int m_Z;


		public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
		{
			// Do not allow editing palettes
			if (brushTarget.layer == 31)
				return;

			Transform erased = GetObjectInCell(grid, brushTarget.transform, new Vector3Int(position.x, position.y, m_Z));
			if (erased != null) {
				Undo.DestroyObjectImmediate (erased.gameObject);
			}

			//int index = Mathf.Clamp(Mathf.FloorToInt(GetPerlinValue(position, m_PerlinScale, k_PerlinOffset)*m_Prefabs.Length), 0, m_Prefabs.Length - 1);
			GameObject prefab = GetCorrectTile(grid, brushTarget.transform, new Vector3Int(position.x, position.y, m_Z));
			GameObject instance = (GameObject) PrefabUtility.InstantiatePrefab(prefab);
			if (instance != null)
			{
				Undo.MoveGameObjectToScene(instance, brushTarget.scene, "Paint Prefabs");
				Undo.RegisterCreatedObjectUndo((Object)instance, "Paint Prefabs");
				instance.transform.SetParent(brushTarget.transform);
				instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3Int(position.x, position.y, m_Z) + new Vector3(.5f, .5f, .5f)));
			}

			CheckTile(grid, brushTarget, new Vector3Int(position.x + 1, position.y, m_Z));
			CheckTile(grid, brushTarget, new Vector3Int(position.x - 1, position.y, m_Z));
			CheckTile(grid, brushTarget, new Vector3Int(position.x, position.y + 1, m_Z));
			CheckTile(grid, brushTarget, new Vector3Int(position.x, position.y - 1, m_Z));
		}


		public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position)
		{
			// Do not allow editing palettes
			if (brushTarget.layer == 31)
				return;

			Transform erased = GetObjectInCell(grid, brushTarget.transform, new Vector3Int(position.x, position.y, m_Z));
			if (erased != null)
				Undo.DestroyObjectImmediate(erased.gameObject);
		}


		private static Transform GetObjectInCell(GridLayout grid, Transform parent, Vector3Int position)
		{
			int childCount = parent.childCount;
			Vector3 min = grid.LocalToWorld(grid.CellToLocalInterpolated(position));
			Vector3 max = grid.LocalToWorld(grid.CellToLocalInterpolated(position + Vector3Int.one));
			Bounds bounds = new Bounds((max + min)*.5f, max - min);

			for (int i = 0; i < childCount; i++)
			{
				Transform child = parent.GetChild(i);
				if (bounds.Contains(child.position))
					return child;
			}
			return null;
		}


		private void CheckTile (GridLayout grid, GameObject brushTarget, Vector3Int position) {
			Transform erased = GetObjectInCell(grid, brushTarget.transform, new Vector3Int(position.x, position.y, m_Z));
			if (erased != null) {
				Undo.DestroyObjectImmediate (erased.gameObject);
			} else {
				return;
			}

			GameObject prefab = GetCorrectTile(grid, brushTarget.transform, new Vector3Int(position.x, position.y, m_Z));
			GameObject instance = (GameObject) PrefabUtility.InstantiatePrefab(prefab);

			if (instance != null)
			{
				Undo.MoveGameObjectToScene(instance, brushTarget.scene, "Paint Prefabs");
				Undo.RegisterCreatedObjectUndo((Object)instance, "Paint Prefabs");
				instance.transform.SetParent(brushTarget.transform);
				instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3Int(position.x, position.y, m_Z) + new Vector3(.5f, .5f, .5f)));
			}
		}


		private GameObject GetCorrectTile (GridLayout grid, Transform parent, Vector3Int position) {
			bool t = false, l = false, b = false, r = false;

			Transform top = GetObjectInCell(grid, parent, new Vector3Int(position.x, (position.y + 1), m_Z));
			Transform left = GetObjectInCell(grid, parent, new Vector3Int((position.x - 1), position.y, m_Z));
			Transform bottom = GetObjectInCell(grid, parent, new Vector3Int(position.x, (position.y - 1), m_Z));
			Transform right = GetObjectInCell(grid, parent, new Vector3Int((position.x + 1), position.y, m_Z));

			if (top != null) t = true; 
			if (left != null) l = true; 
			if (bottom != null) b = true; 
			if (right != null) r = true; 

			if (!t && !l && !b && !r) return topMiddle;
			else if (t && !l && !b && !r) return bottomMiddle;
			else if (!t && l && !b && !r) return topRight;
			else if (!t && !l && b && !r) return topMiddle;
			else if (!t && !l && !b && r) return topMiddle;
			else if (t && l && !b && !r) return bottomRight;
			else if (t && !l && !b && r) return bottomLeft;
			else if (t && !l && b && !r) return middle;
			else if (!t && l && b && !r) return topRight;
			else if (!t && !l && b && r) return topLeft;
			else if (!t && l && !b && r) return topMiddle;
			else if (!t && l && b && r) return topMiddle;
			else if (t && !l && b && r) return middleLeft;
			else if (t && l && !b && r) return bottomMiddle;
			else if (t && l && b && !r) return middleRight;
			else if (t && l && b && r) return middle;
			else return null;
		}


		private static float GetPerlinValue(Vector3Int position, float scale, float offset)
		{
			return Mathf.PerlinNoise((position.x + offset)*scale, (position.y + offset)*scale);
		}
	}

	[CustomEditor(typeof(TileBrush))]
	public class TileBrushEditor : GridBrushEditorBase
	{
		private TileBrush tileBrush { get { return target as TileBrush; } }

		private SerializedProperty m_Prefabs;
		private SerializedObject m_SerializedObject;

		protected void OnEnable()
		{
			m_SerializedObject = new SerializedObject(target);
			m_Prefabs = m_SerializedObject.FindProperty("m_Prefabs");
		}

		public override void OnPaintInspectorGUI()
		{
			m_SerializedObject.UpdateIfRequiredOrScript();
			tileBrush.m_PerlinScale = EditorGUILayout.Slider("Perlin Scale", tileBrush.m_PerlinScale, 0.001f, 0.999f);
			tileBrush.m_Z = EditorGUILayout.IntField("Position Z", tileBrush.m_Z);

			EditorGUILayout.PropertyField(m_Prefabs, true);
			m_SerializedObject.ApplyModifiedPropertiesWithoutUndo();
		}
	}
}
