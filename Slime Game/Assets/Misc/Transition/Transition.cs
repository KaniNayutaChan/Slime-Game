using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool isFadingIn = true;
    Vector4 colorVector = new Vector4();
    public float amountToFade;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorVector.w = spriteRenderer.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingIn)
        {
            if (spriteRenderer.color.a <= 1)
            {
                colorVector.w += amountToFade * Time.deltaTime;
                spriteRenderer.color = colorVector;
            }
            else
            {
                isFadingIn = false;
            }
        }
        else
        {
            if (spriteRenderer.color.a >= 0.001f)
            {
                colorVector.w -= amountToFade * Time.deltaTime;
                spriteRenderer.color = colorVector;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
