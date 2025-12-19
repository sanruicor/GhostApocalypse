using UnityEngine;

public class Label : MonoBehaviour
{
    public float speed = 2.5f;
    public SpriteRenderer sr;
    private float timeToLive = 1.2f;  //Tiempo para que desaparezca y se destruya el objeto

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        Color c = sr.color;
        c.a -= Time.deltaTime / timeToLive;
        sr.color = c;

        if (c.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
