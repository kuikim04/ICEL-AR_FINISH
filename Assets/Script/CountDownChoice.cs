using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownChoice : MonoBehaviour
{
    float currentTime = 0f;
    public float startingtime = 10;
    public TMPro.TextMeshProUGUI timeTxt;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingtime;
    }

    // Update is called once per frame
    void Update()
    {

        currentTime -= 1 * Time.deltaTime;
        timeTxt.text = currentTime.ToString("0");

        if (currentTime <= 0)
            currentTime = 0;

    }
 

   
}
