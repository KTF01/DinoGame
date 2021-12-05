using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class mesh_gen : MonoBehaviour
{

    public int xSize = 200;
    public int zSize = 200;

    public int startX = 0;

    public float amplitude = 2;
    public float zMap = 5;

    GlobalManager gm;

    public float xOffset = 0;

    Mesh mesh;
    Vector3[] vertecies;
    Vector3[] normals;
    int[] indicies;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GlobalManager>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();

    }
    void Update()
    {
        transform.position += new Vector3(-gm.worldSpeed * Time.deltaTime, 0,0);
        if (transform.position.x < -140)
        {
            Destroy(gameObject);
        }
    }

    //private void OnValidate()
    //{
    //    mesh = new Mesh();
    //    GetComponent<MeshFilter>().mesh = mesh;
    //    CreateShape();
    //    UpdateMesh();
    //}

    void CreateShape()
    {
        vertecies = new Vector3[(xSize + 1)*(zSize+1)];
        //normals = new Vector3[(xSize + 1)*(zSize+1)];
        
        for (int i = 0, z = 0; z<=zSize;z++)
        {
            for(int x = startX; x<=xSize; x++)
            {
                float currAmlitude = amplitude;
                if (z < zMap) currAmlitude = 0;
                if (z < zMap + 4 && z > zMap) currAmlitude = amplitude / 2;
                float y = Mathf.PerlinNoise(x* 0.3f+xOffset,z*0.3f)* currAmlitude;
                vertecies[i] = new Vector3(x, y, z);

                //float delta = 0.02f;
                //Vector2 loc = new Vector2(x, z);
                //Vector2 xOffsetLoc = loc + new Vector2(delta, 0);
                //Vector2 zOffsetLoc = loc + new Vector2(0, delta);
                //Vector2 xOffsetLocMinus = loc + new Vector2(-delta, 0);
                //Vector2 zOffsetLocMinus = loc + new Vector2(0, -delta);

                //float xOffsetHeight = Mathf.PerlinNoise(xOffsetLoc.x, xOffsetLoc.y) * currAmlitude;
                //float zOffsetHeight = Mathf.PerlinNoise(zOffsetLoc.x, zOffsetLoc.y) * currAmlitude;

                //float xOffsetHeightMinus = Mathf.PerlinNoise(xOffsetLocMinus.x, xOffsetLocMinus.y) * currAmlitude;
                //float zOffsetHeightMinus = Mathf.PerlinNoise(zOffsetLocMinus.x, zOffsetLocMinus.y) * currAmlitude;

                //Vector3 modelXGrad =  new Vector3(xOffsetLoc.x, xOffsetHeight, xOffsetLoc.y)- new Vector3(xOffsetLocMinus.x, xOffsetHeightMinus, xOffsetLocMinus.y);
                //Vector3 modelYGrad =  new Vector3(zOffsetLoc.x, zOffsetHeight, zOffsetLoc.y) - new Vector3(zOffsetLocMinus.x, zOffsetHeightMinus, zOffsetLocMinus.y);

                //Vector3 normal = Vector3.Normalize(Vector3.Cross(modelXGrad, modelYGrad));
                //normals[i] = currAmlitude!=0? normal:Vector3.up;

                i++;
            }
        }

        indicies = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;
        for(int z = 0; z < zSize; z++)
        {
            for (int x = startX; x < xSize; x++)
            {
                indicies[tris + 0] = vert + 0;
                indicies[tris + 1] = vert + xSize + 1;
                indicies[tris + 2] = vert + 1;
                indicies[tris + 3] = vert + 1;
                indicies[tris + 4] = vert + xSize + 1;
                indicies[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
        
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertecies;
        mesh.triangles = indicies;
        //mesh.normals = normals;
        mesh.RecalculateNormals();
       // Debug.Log(mesh.normals.Length);
        
    }
}
