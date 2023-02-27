using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapInputfiled : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField email;
    public TMP_InputField date;

    public int InputSelect;
    public bool LoginPage;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            InputSelect--;
            if (InputSelect < 2)
                InputSelect = 2;
            
            SelectInputFiled();
            
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelect++;

            if (LoginPage)
            {
                if (InputSelect > 2)
                {
                    InputSelect = 0;
                }       
            }
            if (!LoginPage)
            {
                if (InputSelect > 3)
                {
                    InputSelect = 0;

                }
            }
            SelectInputFiled();
           
        }


        void SelectInputFiled()
        {
            switch (InputSelect)
            {
                case 0: username.Select();
                    break;
                case 1: password.Select();
                    break;
                case 2: email.Select();
                    break;
                case 3: date.Select();
                    break;
            }
        }      

    }
    public void UsernameSelected() => InputSelect = 0;
    public void PasswordSelected() => InputSelect = 1;
    public void EmailSelected() => InputSelect = 2;
    public void DateSelected() => InputSelect = 3;


}
