using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace com.cringejam.sticksandstones {

    public class DoorScaleWall : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private MeshFilter meshFilter = null;
        [SerializeField] private TransformAndMeshFilter doorScale = null;
        [SerializeField] private MeshCollider theMeshCollider = null;

        [Header("Specifications")]
        [SerializeField] private Vector2Int[] wallVerticesToSet = null;

        [Header("Debugging")]
        [SerializeField] private GameObject verticeText = null;

        //Declare privates
        private TransformAndText[] wallVerticeTransformTexts = null;
        private TransformAndText[] doorVerticeTransformTexts = null;
        private Vector3[] wallVertices = null;

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
        private const string WALL = "Wall";
        private const string DOOR = "Door";

        #endregion

        #region Start

        private void Start() {
            //Declare
            int meshFilterLength = meshFilter.mesh.vertices.Length;
            //Check
            if (verticeText != null) {
                //Create transform texts
                CreateTransformTexts(ref wallVerticeTransformTexts, meshFilterLength);
                CreateTransformTexts(ref doorVerticeTransformTexts, doorScale.TheMeshFilter.mesh.vertices.Length);
            }
            //Expand
            wallVertices = new Vector3[meshFilterLength];
            //Loop
            for (int i = 0; i < wallVertices.Length; i++) {
                //Copy
                wallVertices[i] = meshFilter.mesh.vertices[i];
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
                GizmoVertices(wallVerticeTransformTexts, transform, meshFilter.sharedMesh.vertices, WALL);
                GizmoVertices(doorVerticeTransformTexts, doorScale.TheTransform, doorScale.TheMeshFilter.sharedMesh.vertices, DOOR);
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

        public void DoorScaleWallUpdate() {
            //Loop
            for (int i = 0; i < wallVerticesToSet.Length; i++) {
                //Set
                wallVertices[wallVerticesToSet[i].x] =
                    transform.InverseTransformPoint(doorScale.TheTransform.TransformPoint(doorScale.TheMeshFilter.mesh.vertices[wallVerticesToSet[i].y]));
            }
            //Set
            meshFilter.mesh.vertices = wallVertices;
            //Must null so the collider can be set next
            theMeshCollider.sharedMesh = null;
            //Update mesh collider
            theMeshCollider.sharedMesh = meshFilter.mesh;
        }

        #endregion

    }

}