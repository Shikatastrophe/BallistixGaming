using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    public int Player;
    TextMeshProUGUI proUGUI;

    private void OnEnable()
    {
        GameManager.UpdateNumber += UpdateNumber;
    }

    private void OnDisable()
    {
        GameManager.UpdateNumber -= UpdateNumber;
    }

    private void Awake()
    {
        proUGUI = GetComponent<TextMeshProUGUI>();
        proUGUI.text = "20";
    }

    public void UpdateNumber(int ply, int num)
    {
        if (Player == ply)
        {
            if(num < 10)
            {
                proUGUI.text = "0" + num.ToString();
            }
            else
            {
                proUGUI.text = num.ToString();
            }
        }
    }
}
