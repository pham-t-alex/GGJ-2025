using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private float range = 5f;
    private float viewAngleWidth = 60f; // value is angle width centered based on rotation of the object
    [SerializeField] private int rays = 10;

    private Mesh mesh;
    [SerializeField] private MeshFilter filter;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        filter.mesh = mesh;
    }

    void LateUpdate()
    {
        CreateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMesh()
    {
        Vector3[] vertices = new Vector3[rays + 1];
        int[] triangles = new int[(rays - 1) * 3];
        vertices[0] = Vector2.zero;

        // 2 rays on edges, rest of the rays evenly spaced in between
        // so angle between rays is (viewAngleEnd - viewAngleStart) / (rays - 1)
        float angle = viewAngleWidth / (rays - 1);
        for (int i = 0; i < rays; i++)
        {
            float angleDirection = (-viewAngleWidth / 2) + (angle * i);
            Vector3 direction = Quaternion.Euler(0, 0, angleDirection) * transform.right;
            int layerMask = 0;
            layerMask |= 1 << 3; // hit solid objects
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, layerMask);
            if (hit)
            {
                vertices[i + 1] = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                vertices[i + 1] = transform.InverseTransformPoint(transform.position + (direction * range));
            }
            // make triangle with most recent two vertices and the center
            if (i > 0)
            {
                int triangleIndex = (i - 1) * 3;
                triangles[triangleIndex] = 0; // center
                triangles[triangleIndex + 1] = i; // previous vertex
                triangles[triangleIndex + 2] = i + 1; // i + 1 is the current vertex
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshRenderer>().material =  new Material(Shader.Find("Sprites/Default"));
        GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 0.2f);
    }

    public void Initialize(float range, float width)
    {
        this.range = range;
        viewAngleWidth = width;
    }
}
