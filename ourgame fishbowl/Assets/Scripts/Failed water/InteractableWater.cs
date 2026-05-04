
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

// add buoyancy with an effector to make the character float
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
[RequireComponent(typeof(WaterTrigger))]

public class InteractableWater : MonoBehaviour
{
    [Range(2, 500)] public int NumOfVertices = 70;
    public float width = 10f;
    public float height = 4f;
    public Material watermaterial;
    private const int NUM_OF_Y_VERTICES = 2;

    public Color GizmoColor = Color.white;

    private Mesh mesh;
    private MeshRenderer meshrender;
    private MeshFilter meshfilter;
    private Vector3[] vertices;
    private int[] topverticesIndex;

    private EdgeCollider2D coll;

    private void Reset()
    {
        coll = GetComponent<EdgeCollider2D>();
        coll.isTrigger = true;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       GenerateMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetEdgeCollider()
    {
        coll = GetComponent<EdgeCollider2D>();

        Vector2[] newPoints = new Vector2[2];

        Vector2 firstPoint = new Vector2(vertices[topverticesIndex[0]].x, vertices[topverticesIndex[0]].y);
        newPoints[0] = firstPoint;

        Vector2 secondPoint = new Vector2(vertices[topverticesIndex[topverticesIndex.Length - 1]].x, vertices[topverticesIndex[topverticesIndex.Length - 1]].y);
        newPoints[1] = secondPoint;

        coll.offset = Vector2.zero;
        coll.points = newPoints;
    }


    public void GenerateMesh()
    {
        mesh = new Mesh();

        vertices = new Vector3[NumOfVertices * NUM_OF_Y_VERTICES];
        topverticesIndex = new int[NumOfVertices];
        for (int y = 0; y < NUM_OF_Y_VERTICES; y++)
        {
            for (int x = 0; x < NumOfVertices; x++)
            {

                float xPos = (x / (float)(NumOfVertices - 1)) * width - width / 2;
                float yPos = (y / (float)(NUM_OF_Y_VERTICES - 1)) * height - height / 2;

                vertices[y * NumOfVertices + x] = new Vector3(xPos, yPos, 0f);

                if (y == NUM_OF_Y_VERTICES - 1)
                {
                    topverticesIndex[x] = y * NumOfVertices + x;
                }
            }

        }

        int[] triangles = new int[(NumOfVertices - 1) * (NUM_OF_Y_VERTICES - 1) * 6];
        int index = 0;

        for (int y = 0; y < NUM_OF_Y_VERTICES - 1; ++y)
        {
            for (int x = 0; x < NumOfVertices - 1; x++)
            {
                int bottomleft = (y * NumOfVertices + x);
                int bottomRight = bottomleft + 1;

                int topleft = bottomleft + NumOfVertices;
                int topright = topleft + 1;

                triangles[index++] = bottomleft;
                triangles[index++] = topleft;
                triangles[index++] = bottomRight;

                triangles[index++] = bottomRight;
                triangles[index++] = topright;
                triangles[index++] = topleft;


            }
        }
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; ++i) 
        {
            uvs[i] = new Vector2((vertices[i].x + width / 2) / width, (vertices[i].y + height / 2) / height);
        }
        if (meshrender == null)
        
            meshrender = GetComponent<MeshRenderer>();
        
        if (meshfilter == null)
        
            meshfilter = GetComponent<MeshFilter>();
           
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshfilter.mesh = mesh;
            

    }
}
[CustomEditor(typeof(InteractableWater))]
public  class InteractableWaterEditor : Editor
{
    private InteractableWater water;

    private void OnEnable()
    {
        water = (InteractableWater)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        InspectorElement.FillDefaultInspector(root, serializedObject, this);

        root.Add(new VisualElement
        {
            style = { height = 10 }
        });

        Button generateMeshButton = new Button(() => water.GenerateMesh())
        {
            text = "Generate Mesh"

        };
        root.Add(generateMeshButton);

        Button placeEdgeColliderButton = new Button(() => water.ResetEdgeCollider())
        {

            text = "Place Edge Collider"
        };

        root.Add(placeEdgeColliderButton);

        return root;



    }

    private void ChangeDimension(ref float width, ref float height, float calculatewidthMax, float calculateheightMax)
    {
        width = Mathf.Max(0.1f, calculatewidthMax);
        height = Mathf.Max(0.1f, calculateheightMax);
    }
    private void OnSceneGUI()
    {
        Handles.color = water.GizmoColor;
        Vector3 center = water.transform.position;
        Vector3 size = new Vector3(water.width, water.height, 0.1f);
        Handles.DrawWireCube(center, size);

        float handleSize = HandleUtility.GetHandleSize(center) * 0.1f;
        Vector2 snap = Vector3.one * 0.1f;

        Vector3[] corners = new Vector3[4];
        corners[0] = center + new Vector3(-water.width / 2, -water.height / 2, 0);
        corners[1] = center + new Vector3(water.width / 2, -water.height / 2, 0);  
        corners[2] = center + new Vector3(-water.width / 2, water.height / 2, 0);  
        corners[3] = center + new Vector3(water.width / 2, water.height / 2, 0);   

        EditorGUI.BeginChangeCheck();
        Vector3 newBottomLeft = Handles.FreeMoveHandle(corners[0], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeDimension(ref water.width, ref water.height, corners[1].x - newBottomLeft.x, corners[3].y - newBottomLeft.y);
            water.transform.position += new Vector3((newBottomLeft.x - corners[0].x) / 2, (newBottomLeft.y - corners[0].y) / 2, 0);
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newBottomRight = Handles.FreeMoveHandle(corners[1], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeDimension(ref water.width, ref water.height, newBottomRight.x - corners[0].x, corners[3].y - newBottomRight.y);
            water.transform.position += new Vector3((newBottomRight.x - corners[1].x) / 2, (newBottomRight.y - corners[1].y) / 2, 0);

        }

        EditorGUI.BeginChangeCheck();
        Vector3 newTopLeft = Handles.FreeMoveHandle(corners[2], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {

            ChangeDimension(ref water.width, ref water.height, corners[3].x - newTopLeft.x, newTopLeft.y - corners[0].y);
            water.transform.position += new Vector3((newTopLeft.x - corners[2].x) / 2, (newTopLeft.y - corners[2].y) / 2, 0);
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newTopRight = Handles.FreeMoveHandle(corners[3], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeDimension(ref water.width, ref water.height, newTopRight.x - corners[2].x, newTopRight.y - corners[1].y);
            water.transform.position += new Vector3((newTopRight.x - corners[3].x) / 2, (newBottomRight.y - corners[3].y) / 2, 0);

        }


        if (GUI.changed) 
        { 
            water.GenerateMesh();
        }

    }
}

