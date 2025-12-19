using UnityEngine;

public class GhostBarrierDetector : MonoBehaviour
{
    private Animator animator;
    private Ghost ghost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        ghost = GetComponent<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Destroy(gameObject);
            }

            return;
        }
        
        //Comprobamos si debemos responder al superpoder del jugador
        //Para ello el jugador tiene que tener disponible el superpoder
        //y además el fantasma no puede haber superado ya la barrera
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.HasSuperPower && !ghost.IsLNotifiedLife)
        {
            StartDestruction();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("[Ghost] OnTriggerEnter2D");

        // Comprobar que el otro objeto tiene el tag "Player"
        if (collision.CompareTag("Player"))
        {
            StartDestruction();
        }
    }

    private void StartDestruction()
    {
        // Hacemos que el fantasma se quede quieto
        ghost.Dead();

        // Iniciamos la animacion de explosion
        animator.SetTrigger("Explosion");
    }

    public void DestroyGhost()
    {
        // Destruimos el fantasma
        Destroy(gameObject);
    }
}
