using UnityEngine;
using UnityEngine.UI;

public class MedalSystem : MonoBehaviour
{
    public Image medalImage;
    public Sprite gold;
    public Sprite silver;
    public Sprite bronze;

    public void SetMedal(int score, int total)
    {
        float percent = (float)score / total;

        if (percent >= 0.8f)
        {
            medalImage.sprite = gold;
        }
        else if (percent >= 0.5f)
        {
            medalImage.sprite = silver;
        }
        else
        {
            medalImage.sprite = bronze;
        }
    }
}