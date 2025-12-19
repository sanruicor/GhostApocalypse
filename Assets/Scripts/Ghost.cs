using UnityEngine;


public class Ghost : MonoBehaviour
{
    protected float speed = 6f;
    protected int points = 100;
    protected bool isAlive = true;
    protected bool isLNotifiedLife = false; //Indica que ya se ha causado la pérdida de una vida y no se debe restar más
    protected Vector3 movementDirection;
    public GameObject label100Prefab;
    public GameObject label150Prefab;


    public bool IsLNotifiedLife => isLNotifiedLife;

    public void Stop()
    {
        movementDirection = Vector3.zero;
    }

    public void Dead()
    {
        Stop();
        isAlive = false;
        //Cuando el fantasma es alcanzado por el jugador se computa la correspondiente puntuación
        GameManager.instance.AddPoints(points);

        if (points == 100)
        {
            Instantiate(label100Prefab, transform.position + Vector3.up * 0.7f, Quaternion.identity);
        } 
        else if (points == 150)
        {
            Instantiate(label150Prefab, transform.position + Vector3.up * 0.7f, Quaternion.identity);
        }
    }
}
