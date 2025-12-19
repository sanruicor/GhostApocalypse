using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public ScoreDigit[] digits;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetScore(0);  
    }

    public void SetScore(int score)
    {
        if (score < 0 || score > 99999)
        {
            return;
        }

        for (int i = 0; i < digits.Length; i++)
        {
            int digito = score % 10;
            score /= 10;
            digits[i].SetDigit(digito);
        }

    }
}
