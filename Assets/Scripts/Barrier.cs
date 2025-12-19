using UnityEngine;

public class Barrier : MonoBehaviour
{
    private float speed = 8f;
    private float movement = 0f;
    public const float maxMovementHeight = 4f;
    public const float minMovementHeight = -4f;
    public const float maxReach = 5f;
    public const float minReach = -5f;
    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                transform.position = startPosition;
            }

            return;
        }
        
        // Movimientos de la barrera
        if(Input.GetKey(KeyCode.UpArrow)) {
            if(movement == -1f) {
                movement = 0f;
            } else {
                movement = 1f;
            }
        } 
        
        if(Input.GetKey(KeyCode.DownArrow)) {
            if(Input.GetKey(KeyCode.UpArrow)) {
                movement = 0f;
            } else {
                movement = -1f;
            }
        }

        if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)){
            movement = 0f;
        }

        // Límites de la barrera
        Vector3 newPosition = transform.position + Vector3.up * movement * speed * Time.deltaTime;

        newPosition.y = Mathf.Clamp(newPosition.y, minMovementHeight, maxMovementHeight);

        transform.position = newPosition;
    }
}
