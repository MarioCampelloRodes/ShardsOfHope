using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI powerUpNameText;

    private void Start()
    {
        HidePowerUp();
    }

    public void ShowPowerUp(PowerUp powerUp)
    {
        iconImage.sprite = powerUp.powerUpIcon;
        powerUpNameText.text = powerUp.powerUpName;

        iconImage.enabled = true;
        powerUpNameText.enabled = true;
    }

    public void HidePowerUp()
    {
        iconImage.enabled = false;
        powerUpNameText.enabled = false;
    }
}
