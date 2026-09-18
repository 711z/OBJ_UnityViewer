using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjLoader : MonoBehaviour
{
    [Header("放在StreamingAssets里的OBJ文件名(带后缀)")]
    public string objName = "cube.obj";

    private Mesh modelMesh;

    void Start()
    {
        LoadOBJ();
    }

    void LoadOBJ()
    {
        string path = Application.streamingAssetsPath + "/" + objName;
        string text = System.IO.File.ReadAllText(path);

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();

        string[] lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            string[] word = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (word.Length == 0) continue;

            if (word[0] == "v")
            {
                float x = float.Parse(word[1]);
                float y = float.Parse(word[2]);
                float z = float.Parse(word[3]);
                verts.Add(new Vector3(x, y, z));
            }

            if (word[0] == "f")
            {
                int p0 = int.Parse(word[1].Split('/')[0]) - 1;
                int p1 = int.Parse(word[2].Split('/')[0]) - 1;
                int p2 = int.Parse(word[3].Split('/')[0]) - 1;

                tris.Add(p0);
                tris.Add(p1);
                tris.Add(p2);
            }
        }

        modelMesh = new Mesh();
        modelMesh.vertices = verts.ToArray();
        modelMesh.triangles = tris.ToArray();
        modelMesh.RecalculateNormals();
        modelMesh.RecalculateBounds();

        GameObject obj = new GameObject("动态加载OBJ模型");
        obj.transform.position = new Vector3(0, 0, 2);
        obj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();

        mf.mesh = modelMesh;
        mr.material = new Material(Shader.Find("Standard"));

        Debug.Log("✅模型顶点总数量：" + modelMesh.vertices.Length);
        Debug.Log("✅第一个顶点坐标：" + modelMesh.vertices[0]);
    }

    public Vector3[] GetAllVertex()
    {
        if (modelMesh == null) return null;
        return modelMesh.vertices;
    }
}