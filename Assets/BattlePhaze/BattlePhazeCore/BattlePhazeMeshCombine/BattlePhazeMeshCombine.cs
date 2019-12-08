using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattlePhaze.MeshOptimization.Setup.Combine
{
    /// <summary>
    /// Mesh Combine System
    /// </summary>
    public class BattlePhazeMeshCombine : MonoBehaviour
    {
        /// <summary>
        /// Mesh combine and material application
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="Renders"></param>
        public void MeshCombine(MeshFilter[] meshFilters, MeshRenderer[] Renders)
        {
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);

                i++;
            }
            transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
            transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
            transform.gameObject.SetActive(true);
        }
    }
}
