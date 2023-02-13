using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HideText : MonoBehaviour
{
    public TextMeshProUGUI txt;


    public void Hidext()
    {
        txt.text = "";
    }
}
