using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderPercentage = 0.05f;
    private bool canMove = false;

    public float scrollSpeed = 5f;

    public Vector3 defaultPosition = new Vector3(-22.5f, 70, -10);
    private float minX = -62.5f;
    private float maxX = 22.5f;
    private float minY = 20f;
    private float maxY = 90f;
    private float minZ = -55f;
    private float maxZ = 35f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            canMove = !canMove;
        }

        if (!canMove)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= (Screen.height - Screen.height * panBorderPercentage))
        {
            if (transform.position.z < maxZ)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }

        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= (Screen.height * panBorderPercentage))
        {
            if (transform.position.z > minZ)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= (Screen.width - Screen.width * panBorderPercentage))
        {
            if (transform.position.x < maxX)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
        }
        if (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= (Screen.width * panBorderPercentage))
        {
            if (transform.position.x > minX)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            transform.position = defaultPosition;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
