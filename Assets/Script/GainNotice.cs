using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainNotice : MonoBehaviour
{
    public float disableTime;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Disable), disableTime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
