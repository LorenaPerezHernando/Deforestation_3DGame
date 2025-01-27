using UnityEngine;
using System;
using System.Collections.Generic;

namespace Deforestation
{

	public class TreeTerrainController : MonoBehaviour
	{
		#region Properties
		public TreeInstance[] Trees => _trees;
		#endregion

		#region Fields
		[SerializeField] private Tree _treeDetectionPrefab;
		[SerializeField] private Tree _treePrefab;
		private TreeInstance[] _trees;
		Terrain _terrain;
		#endregion

		#region Unity Callbacks
		// Start is called before the first frame update
		void Start()
		{
			_terrain = Terrain.activeTerrain;
			_trees = _terrain.terrainData.treeInstances;

			InitializeTrees();
		}

		private void InitializeTrees()
		{
			for (int i = _trees.Length - 1; i >= 0; i--)
			{
				TreeInstance tree = _trees[i];
				Vector3 treeWorldPos = TreeToWorldPosition(tree);
				Tree treeDetector = Instantiate(_treeDetectionPrefab, treeWorldPos, Quaternion.identity);
				treeDetector.transform.parent = transform;
				treeDetector.Index = i;
			}
		}

		public GameObject DestroyTree(int i, Vector3 treeWorldPos)
		{
			//create tree
			Tree newTree = Instantiate(_treePrefab, treeWorldPos, Quaternion.identity);

			RemoveTreeFromTerrain(i);
			return newTree.gameObject;
		}

		void OnDestroy()
		{
			_terrain.terrainData.treeInstances = _trees;
		}
		#endregion

		#region Public Methods
		public Vector3 TreeToWorldPosition(TreeInstance tree)
		{
			return Vector3.Scale(tree.position, _terrain.terrainData.size) + _terrain.transform.position;
		}
		public void RemoveTreeFromTerrain(int index)
		{
			//TODO: Reasignar todos los indices de todos los tree detectors.
			List<TreeInstance> trees = new List<TreeInstance>(_terrain.terrainData.treeInstances);
			trees.RemoveAt(index);
			_terrain.terrainData.treeInstances = trees.ToArray();
		}
		#endregion

		#region Private Methods

		#endregion
	}
}
