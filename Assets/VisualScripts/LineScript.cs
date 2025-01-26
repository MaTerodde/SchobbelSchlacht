using UnityEngine;

public class LineScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private bool isConnected;
    private Vector3[] positions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        isConnected = false;
        lineRenderer.enabled = false;
        positions = new Vector3[2];
    }


    public void AttachHook(Vector3 target)
    {
        if (target != null)
        {
            positions[0] = transform.position;
            positions[1] = target;
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(positions);
            isConnected = true;
        }
    }

    public void DetachHook()
    {
        lineRenderer.enabled = false;
        isConnected = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isConnected)
        {
            lineRenderer.SetPosition(0,transform.position);
        }
    }
}
