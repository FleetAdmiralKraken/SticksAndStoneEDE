using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace com.cringejam.sticksandstones {

    public class FloorScaleSpikesFloor : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private MeshFilter meshFilter = null;
        [SerializeField] private TransformAndMeshFilter floorScaleSpikes = null;
        [SerializeField] private MeshCollider theMeshCollider = null;

        [Header("Specifications")]
        [SerializeField] private Vector2Int[] floorVerticesToSet = null;

        [Header("Debugging")]
        [SerializeField] private GameObject verticeText = null;

        //Declare privates
        private TransformAndText[] floorVerticeTransformTexts = null;
        private TransformAndText[] pitVerticeTransformTexts = null;
        private Vector3[] floorVertices = null;

        #endregion

        #region Classes

        [Serializable]
        private class TransformAndMeshFilter {
            //Declare
            public Transform TheTransform = null;
            public MeshFilter TheMeshFilter = null;
        }

        [Serializable]
        private class TransformAndText {
            //Declare
            public Transform TheTransform = null;
            public TextMeshPro TheText = null;
            //Constructor
            public TransformAndText(Transform theTransform, TextMeshPro theText) {
                //Set
                TheTransform = theTransform;
                TheText = theText;
            }
        }

        #endregion

        #region Constants

        //Declare privates
        private const string FLOOR = "Floor";
        private const string PIT = "Pit";

        #endregion

        #region Start

        private void Start() {
            //Declare
            int meshFilterLength = meshFilter.mesh.vertices.Length;
            //Check
            if (verticeText != null) {
                //Create transform texts
                CreateTransformTexts(ref floorVerticeTransformTexts, meshFilterLength);
                CreateTransformTexts(ref pitVerticeTransformTexts, floorScaleSpikes.TheMeshFilter.mesh.vertices.Length);
            }
            //Expand
            floorVertices = new Vector3[meshFilterLength];
            //Loop
            for (int i = 0; i < floorVertices.Length; i++) {
                //Copy
                floorVertices[i] = meshFilter.mesh.vertices[i];
            }
        }

        private void CreateTransformTexts(ref TransformAndText[] transformAndTexts, int length) {
            //Expand
            transformAndTexts = new TransformAndText[length];
            //Loop
            for (int i = 0; i < transformAndTexts.Length; i++) {
                //Create
                GameObject textGameObject = Instantiate(verticeText);
                //Create
                transformAndTexts[i] = new TransformAndText(textGameObject.transform, textGameObject.GetComponent<TextMeshPro>());
            }
        }

        #endregion

        #region Gizmos

        #if UNITY_EDITOR

            private void OnDrawGizmos() {
                //Gizmo vertices
                GizmoVertices(floorVerticeTransformTexts, transform, meshFilter.sharedMesh.vertices, FLOOR);
                GizmoVertices(pitVerticeTransformTexts, floorScaleSpikes.TheTransform, floorScaleSpikes.TheMeshFilter.sharedMesh.vertices, PIT);
            }

            private void GizmoVertices(TransformAndText[] transformAndTexts, Transform theTransform, Vector3[] vertices, string strName) {
                //Check
                if (transformAndTexts != null) {
                    //Loop
                    for (int i = 0; i < transformAndTexts.Length; i++) {
                        //Move
                        transformAndTexts[i].TheTransform.position = theTransform.TransformPoint(vertices[i]);
                        //Declare
                        string strNumber = i.ToString();
                        //Change name
                        transformAndTexts[i].TheTransform.name = strName + strNumber;
                        //Change text
                        transformAndTexts[i].TheText.text = strNumber;
                    }
                }
            }

        #endif

        #endregion

        #region Public functions

        public void FloorScaleSpikesFloorUpdate() {
            //Loop
            for (int i = 0; i < floorVerticesToSet.Length; i++) {
                //Set
                floorVertices[floorVerticesToSet[i].x] =
                    transform.InverseTransformPoint(floorScaleSpikes.TheTransform.TransformPoint(floorScaleSpikes.TheMeshFilter.mesh.vertices[floorVerticesToSet[i].y]));
            }
            //Set
            meshFilter.mesh.vertices = floorVertices;
            //Must null so the collider can be set next
            theMeshCollider.sharedMesh = null;
            //Update mesh collider
            theMeshCollider.sharedMesh = meshFilter.mesh;
        }

        #endregion

    }

}