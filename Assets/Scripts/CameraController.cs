using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float panSpeed = 30f;
    [SerializeField]
    private float panBorderPercentage = 0.05f;
    [SerializeField]
    public float scrollSpeed = 5f;
    [SerializeField]
    public Vector3 defaultPosition;

    private bool canMove = false;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float minZ;
    private float maxZ;


    private void Start()
    {
        minX = defaultPosition.x - 20f;
        maxX = defaultPosition.x + 20f;

        minY = defaultPosition.y - 35f;
        maxY = defaultPosition.y + 5f;

        minZ = defaultPosition.z - 20f;
        maxZ = defaultPosition.z + 20f;
    }


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

        this.HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= (Screen.height - Screen.height * panBorderPercentage))
        {
            if (transform.position.z < maxZ)
            {
                transform.Translate(panSpeed * Time.deltaTime * Vector3.forward, Space.World);
            }

        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= (Screen.height * panBorderPercentage))
        {
            if (transform.position.z > minZ)
            {
                transform.Translate(panSpeed * Time.deltaTime * Vector3.back, Space.World);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= (Screen.width - Screen.width * panBorderPercentage))
        {
            if (transform.position.x < maxX)
            {
                transform.Translate(panSpeed * Time.deltaTime * Vector3.right, Space.World);
            }
        }
        if (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= (Screen.width * panBorderPercentage))
        {
            if (transform.position.x > minX)
            {
                transform.Translate(panSpeed * Time.deltaTime * Vector3.left, Space.World);
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
