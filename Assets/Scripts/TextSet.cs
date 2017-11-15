using UnityEngine;
using UnityEngine.UI;
using System;

public class TextSet : MonoBehaviour
{
    [SerializeField] private Text desplayText;
    [SerializeField] private string prefixText;

    public void UpdateText(string newVal)
    {
        desplayText.text = prefixText + newVal;
    }

    public void UpdateText(float newVal)
    {
        desplayText.text = prefixText + Math.Round( newVal,2);
    }
    public void UpdateText(float newVal,int decimalAmount)
    {
        desplayText.text = prefixText + Math.Round(newVal, decimalAmount);
    }
}
