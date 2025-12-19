using UnityEngine;

public class PinkGhost : Ghost
{
    private Vector3 movementTarget;
    private bool onTarget; //Indica que el fantasma ya está en la posición destino y no debe hacer nada antes de realizar un nuevo movimiento
    private float distanceTraveled;
    private float stepLength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        points = 150;
        StartMovement();
    }

    private void StartMovement()
    {
        //Controlamos que el fantasma está vivo
        //Puede no estar vivo si se ha chocado con la barrera en el mismo momento en que se ha invocado a StartMovement
        if (!isAlive)
        {
            return;
        }

        distanceTraveled = 0f;
        bool validMovementDirection = false;

        while (!validMovementDirection)
        {
            float r = Random.Range(0f, 1f);
            if (r < 0.5f)
            {
                movementDirection = Vector3.right;
                validMovementDirection = true;
            }
            else if (r < 0.75f)
            {
                movementDirection = Vector3.up;
            }
            else
            {
                movementDirection = Vector3.down;
            }

            stepLength = Random.Range(0.5f, 1.2f);

            movementTarget = transform.position + movementDirection * stepLength;

            if (movementTarget.y <= Barrier.maxReach && movementTarget.y >= Barrier.minReach)
            {
                validMovementDirection = true;
            }
        }

        onTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameOver)
        {
            return;
        }

        transform.position += movementDirection * speed * Time.deltaTime;
        if (!onTarget)
        {
            distanceTraveled += speed * Time.deltaTime;
        }

        if (distanceTraveled >= stepLength)
        {
            transform.position = movementTarget;
            Stop();
            if (!onTarget)
            {
                Invoke(nameof(StartMovement), Random.Range(0.5f, 1f));
                onTarget = true;
            }
        }

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
