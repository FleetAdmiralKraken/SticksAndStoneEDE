using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.cringejam.sticksandstones {

    public class DoorScaleWall : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private MeshFilter meshFilter = null;
        [SerializeField] private TransformAndMeshFilter doorScale = null;
        [SerializeField] private TransformAndMeshFilter floor = null;

        #endregion

        #region Classes

        [Serializable]
        private class TransformAndMeshFilter {
            //Declare
            public Transform TheTransform = null;
            public MeshFilter TheMeshFilter = null;
        }

        #endregion

        #region Public functions

        public void DoorScaleWallUpdate() {
            //Declare
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[4];
            //Create vertices
            vertices[0] = doorScale.TheTransform.TransformPoint(doorScale.TheMeshFilter.mesh.vertices[0]);

            vertices[0] = transform.InverseTransformPoint(vertices[0]);

            GameObject objTest = new GameObject("Test");
            objTest.transform.position = vertices[0];
            Destroy(objTest, 0.1f);
        }

        #endregion

    }

}