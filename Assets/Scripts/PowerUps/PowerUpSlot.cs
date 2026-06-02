using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSlot : MonoBehaviour
{
    public PowerUp CurrentPowerUp { get; private set; }

    [SerializeField] private PowerUpUI powerUpUI;

    public bool HasPowerUp => CurrentPowerUp != null;

    private void Start()
    {
        powerUpUI.HidePowerUp();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            UsePowerUp();
        }
    }
    public void StorePowerUp(PowerUp newPowerUp)
    {
        CurrentPowerUp = newPowerUp;

        powerUpUI.ShowPowerUp(CurrentPowerUp);
    }

    public void UsePowerUp()
    {
        if (CurrentPowerUp == null)
            return;

        CurrentPowerUp.Use();

        CurrentPowerUp = null;

        powerUpUI.HidePowerUp();

    }
}
