using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    Image image;
    bool isFadingIn = true;
    Vector4 colorVector = new Vector4();
    public float amountToFade;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        colorVector.w = image.color.a;
    }

    // Update is called once per frame
    void Update()
    {    
        if (isFadingIn)
        {
            if (image.color.a <= 1)
            {
                colorVector.w += amountToFade * Time.deltaTime;
                image.color = colorVector;
            }
            else
            {
                isFadingIn = false;
            }
        }
        else if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            if (image.color.a >= 0.001f)
            {
                colorVector.w -= amountToFade * Time.deltaTime;
                image.color = colorVector;
            }
            else
            {
                Destroy(GetComponentInParent<Canvas>().gameObject);
            }
        }
    }
}
