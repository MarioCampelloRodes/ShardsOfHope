using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScore : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float elapsedTime;

    void Update()
    {
        //sumamos el tiempo que pasa en cada frame
        elapsedTime += Time.deltaTime;

        //calcular segundos y centesim
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int hundredths = Mathf.FloorToInt((elapsedTime * 100) % 100);

        //actualizar el teshto
        timerText.text = string.Format("{0:00}:{1:00}", seconds, hundredths);
    }
}