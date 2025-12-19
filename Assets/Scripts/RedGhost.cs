using UnityEngine;

public class RedGhost : Ghost
{
    private Vector3 maxExitHeight = new Vector3(8.5f, 5f, 0);
    private Vector3 minExitHeight = new Vector3(8.5f, -5f, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementDirection = Vector3.right;
        points = 100;
        // Decidimos si el fantasma se mueve en diagonal
        if (Random.Range(0f, 1f) < 0.1f)
        {
            movementDirection = GetRandomDirection();
            points = 150;
        }
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 aimingPoint = Vector3.Lerp(maxExitHeight, minExitHeight, Random.Range(0f, 1f));
        Vector3 aimingDirection = aimingPoint - transform.position;

        return aimingDirection.normalized;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameOver)
        {
            return;
        }
        
        transform.position += movementDirection * speed * Time.deltaTime;

        //Comprobamos si ya hemos superado la barrera, para restar la vida
        if (transform.position.x > 10 && !isLNotifiedLife)
        {
            GameManager.instance.LooseLife();
            isLNotifiedLife = true;
        }

        if (transform.position.x > 20)
        {
            // Destruimos el fantasma
            Destroy(gameObject);
        }
    }
}
