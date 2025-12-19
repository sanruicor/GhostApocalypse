using UnityEngine;

public class ScoreDigit : MonoBehaviour
{
    public GameObject[] digits;

    public void SetDigit(int activeDigit)
    {   
        if (activeDigit < 0 || activeDigit >= digits.Length)
        {
            return;
        }
        
        foreach (GameObject d in digits)
        {
            d.SetActive(false);
        }

        digits[activeDigit].SetActive(true);
    }
}
