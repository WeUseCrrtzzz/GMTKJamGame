using UnityEngine;

public class DrawRuntimeBox : MonoBehaviour
{
    public float XRadius = 5f;
    public float ZRadius = 5f;
    public Color lineColor = Color.green;

    private Material lineMaterial;

    void Start()
    {
        // Create a simple colored material for drawing
        lineMaterial = new Material(Shader.Find("Sprites/Default"));
        lineMaterial.color = lineColor;
    }

    void OnRenderObject()
    {
        if (!lineMaterial) return;

        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(lineColor);

        Vector3 pos = transform.position;

        // Calculate corners
        Vector3 p1 = pos + new Vector3(-XRadius, 0, -ZRadius);
        Vector3 p2 = pos + new Vector3(-XRadius, 0, ZRadius);
        Vector3 p3 = pos + new Vector3(XRadius, 0, ZRadius);
        Vector3 p4 = pos + new Vector3(XRadius, 0, -ZRadius);

        // Draw edges
        DrawLine(p1, p2);
        DrawLine(p2, p3);
        DrawLine(p3, p4);
        DrawLine(p4, p1);

        GL.End();
    }

    void DrawLine(Vector3 a, Vector3 b)
    {
        GL.Vertex(a);
        GL.Vertex(b);
    }
}
