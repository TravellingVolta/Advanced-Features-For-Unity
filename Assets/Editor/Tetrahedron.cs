using UnityEditor;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tetrahedron : ScriptableWizard
{

    // 这个变量会出现在向导中
    public Vector3 size = new Vector3(1, 1, 1);

    // 最关键的是静态方法和MenuItem特性
    [MenuItem("GameObject/3D Object/Tetrahedron")]
    static void ShowWizard()
    {
        // 第一参数是向导窗口标题，第二个参数是按钮文本
        ScriptableWizard.DisplayWizard<Tetrahedron>("Create Tetrahedron", "Create");
    }

    // 当用户点击创建按钮时调用
    void OnWizardCreate()
    {

        // 创建网格
        var mesh = new Mesh();

        // 创建金字塔的4个顶点
        Vector3 p0 = new Vector3(0, 0, 0);
        Vector3 p1 = new Vector3(1, 0, 0);
        Vector3 p2 = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
        Vector3 p3 = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);

        // 根据输入的size进行缩放
        p0.Scale(size);
        p1.Scale(size);
        p2.Scale(size);
        p3.Scale(size);

        // 将缩放后的顶点列表提供给网格
        mesh.vertices = new Vector3[] { p0, p1, p2, p3 };

        // 提供连接每个顶点的三角形列表
        mesh.triangles = new int[] {
            0,1,2,
            0,2,3,
            2,1,3,
            0,3,1
        };

        // 用这些数据更新网格上的附加数据
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        ;

        // 创建使用这个网格的游戏对象
        var gameObject = new GameObject("Tetrahedron");
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));

    }

    // 每当用户更改向导中的任何内容时调用
    void OnWizardUpdate()
    {

        // 检查以确保提供的值是有效的
        if (this.size.x <= 0 ||
            this.size.y <= 0 ||
            this.size.z <= 0)
        {

            // 当isValid为false时，不能单击创建按钮
            this.isValid = false;

            // 说明不能创建的原因
            this.errorString = "Size cannot be less than zero";

        }
        else
        {

            // 用户可以点击时，开启按钮清空错误消息
            this.errorString = null;
            this.isValid = true;
        }
    }
}
