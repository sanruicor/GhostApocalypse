using UnityEngine;

public class LifeBoard : MonoBehaviour
{
    public GameObject[] lifes;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject go in lifes)
        {
            go.SetActive(true);
        }
    }

    public void SetLifeCount(int lifeCount)
    {
        if (lifeCount < 0 || lifeCount > lifes.Length)
        {
            return;
        }

        for (int i = 0; i < lifes.Length; i++)
        {
            lifes[i].SetActive(i < lifeCount);
        }
    }
}
