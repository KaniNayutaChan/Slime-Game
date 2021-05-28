using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnlargenSkill : BaseSkill
{
    public float amountToIncrease;
    public float amountToDecrease;
    public float startTimeTillIncrease;
    public float startTimeTillDecrease;
    public float maxSize;

    bool isIncreasing = true;
    Vector3 sizeVector = new Vector3();


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        sizeVector.Set(amountToIncrease, amountToIncrease, amountToIncrease);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isIncreasing)
        {
            if (startTimeTillIncrease > 0)
            {
                startTimeTillIncrease -= Time.deltaTime;
            }
            else if (transform.localScale.x < maxSize)
            {
                transform.localScale += sizeVector * Time.deltaTime;
            }
            else
            {
                isIncreasing = false;
                sizeVector.Set(-amountToDecrease, -amountToDecrease, -amountToDecrease);
            }
        }

        if (!isIncreasing)
        {
            if (startTimeTillDecrease > 0)
            {
                startTimeTillDecrease -= Time.deltaTime;
            }
            else if (transform.localScale.x > 0.1f)
            {
                transform.localScale += sizeVector * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
