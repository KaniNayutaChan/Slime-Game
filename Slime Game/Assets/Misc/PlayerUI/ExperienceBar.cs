using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.level == Player.instance.maxLevel)
        {
            image.fillAmount = 1;
        }
        else
        {
            image.fillAmount = Player.instance.experience / Mathf.Pow(2, Player.instance.level);
        }
    }
}

