using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    public float currentTime;
    public float dayLengthMinutes;

    float rotationSpeed;

    [SerializeField] TextMeshProUGUI timeText;

    float midday;
    float translateTime;

    string AMPM = "AM";

    void Start()
    {
        rotationSpeed = 360 / dayLengthMinutes / 60;
        midday = dayLengthMinutes * 60 / 2;
    }

    // Update is called once per frame
    void Update()
    {
        #region TempsFísic
        currentTime += 1 * Time.deltaTime;
        translateTime = (currentTime / (midday * 2));

        float t = translateTime * 24f;
        float hours = Mathf.Floor(t);
        #endregion

        #region TempsText
        string displayHours = hours.ToString();
        if (hours == 0)
        {
            displayHours = "12";
        }
        if (hours > 12)
        {
            displayHours = (hours - 12).ToString();
        }
        if (currentTime>=midday)
        {
            if(AMPM != "PM")
            {
                AMPM = "PM";
            }
        }
        if (currentTime >= midday * 2)
        {
            if (AMPM != "AM")
            {
                AMPM = "AM";
            }
            currentTime = 0;
        }

        #endregion

        t *= 60;
        float minutes = Mathf.Floor(t % 60);

        string displayMinutes = minutes.ToString();

        if(minutes < 10)
        {
            displayMinutes = "0" + minutes.ToString();
        }
        string displayTime = displayHours + ":" + displayMinutes + " " + AMPM;
        timeText.text = displayTime;

        transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);
    }
}
