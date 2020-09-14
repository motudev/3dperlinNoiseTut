using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class perlinNoiseGizmo : MonoBehaviour
{
    public bool cubeBoundries = false;
    public int size = 10;
    public int height = 20;
    public int width = 20;
    public int depth = 20;

    public float frequency = 1.01f;
    private Vector3[] points;

    void Start()
    {
        InstantiatePoints();
    }

    private void FixedUpdate()
    {
        UpdateMesh();
    }
    void UpdateMesh()
    {
        if (cubeBoundries)
        {
            height = size;
            width = size;
            depth = size;
        }
        InstantiatePoints();
    }

    void InstantiatePoints()
    {
        points = new Vector3[height * width * depth];

        for (int z = 0, i = 0; z < depth; z++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    points[i] = new Vector3(x, y, z);
                    i++;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (points != null)
        {
            for (int i = 0; i < points.Length; i++)
            {

                float sample = perlinNoise.get3DPerlinNoise(points[i], frequency);

                sample = (sample + 1f) / 2f;
                Gizmos.color = new Color(sample, sample, sample, 1);

                Gizmos.DrawSphere(points[i], 0.3f);
            }
        }
    }

    [CustomEditor(typeof(perlinNoiseGizmo
))]
    public class terrainEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            var myScript = target as perlinNoiseGizmo
        ;

            myScript.cubeBoundries = GUILayout.Toggle(myScript.cubeBoundries, "Enable Box Size:");

            if (!myScript.cubeBoundries)
            {
                myScript.depth = EditorGUILayout.IntSlider("Depth:", myScript.depth, 1, 30);
                myScript.height = EditorGUILayout.IntSlider("Heigth:", myScript.height, 1, 30);
                myScript.width = EditorGUILayout.IntSlider("Width:", myScript.width, 1, 30);
            }
            else
            {
                myScript.size = EditorGUILayout.IntSlider("Box Size:", myScript.depth, 1, 30);
            }
            myScript.frequency = EditorGUILayout.FloatField("Frequency:", myScript.frequency);


        }
    }

}
