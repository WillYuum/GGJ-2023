using UnityEngine;

public class LatticeGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float triangleSize = 1.0f;

    private Vector3[,] latticeArray;

    private void OnDrawGizmos()
    {
        if (latticeArray == null) return;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * triangleSize, y * triangleSize * Mathf.Sqrt(3) / 2, 0.0f);
                Gizmos.color = Color.red;

                Vector3 p1 = position + new Vector3(-triangleSize / 2.0f, -triangleSize / 2.0f * Mathf.Sqrt(3), 0.0f);
                Vector3 p2 = position + new Vector3(triangleSize / 2.0f, -triangleSize / 2.0f * Mathf.Sqrt(3), 0.0f);
                Vector3 p3 = position + new Vector3(0.0f, triangleSize * Mathf.Sqrt(3) / 2, 0.0f);
                Gizmos.DrawLine(transform.position + p1, transform.position + p2);
                Gizmos.DrawLine(transform.position + p2, transform.position + p3);
                Gizmos.DrawLine(transform.position + p3, transform.position + p1);
            }
        }
    }

    void Start()
    {
        latticeArray = new Vector3[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                latticeArray[x, y] = new Vector3(x * triangleSize, y * triangleSize * Mathf.Sqrt(3) / 2, 0.0f);
            }
        }
    }
}