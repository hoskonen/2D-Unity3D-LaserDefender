using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float spaceShipPadding = 1f;

    private float xMinimum;
    private float xMaximum;
    private float yMinimum;
    private float yMaximum;

    private void Start()
    {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        if (gameCamera != null) xMinimum = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spaceShipPadding;
        if (gameCamera != null) xMaximum = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spaceShipPadding;
        if (gameCamera != null) yMinimum = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + spaceShipPadding;
        if (gameCamera != null) yMaximum = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - spaceShipPadding;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXposition = Mathf.Clamp(transform.position.x + deltaX, xMinimum, xMaximum);
        var newYposition = Mathf.Clamp(transform.position.y + deltaY, yMinimum, yMaximum);

        transform.position = new Vector2(newXposition, newYposition);
    }
}