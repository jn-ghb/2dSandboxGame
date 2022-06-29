using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class DateManager : MonoBehaviour
{
    public string _currentDayType;
    public float _currentTime;

    public Light _dayLight;

    //Light.Color
    Color _dayTimeColor = new Color(1f, 1f, 1f, 1);
    Color _eveningColor = new Color(0.972549f, 0.5529412f, 0.509804f, 1);
    Color _nightColor = new Color(0.2156863f, 0.4627451f, 0.9803922f, 1);

    //Light.intensity
    float _dayTimeIntensity = 1.39f;
    float _eveningIntensity = 1.24f;
    float _nightTimeIntensity = 0.97f;

    //表示間隔
    int _DAYTIME_INTERVAL = 15;
    int _EVENING_FROM_DAYTIME_INTERVAL = 3;
    int _EVENING_INTERVAL = 5;
    int _NIGHT_FROM_EVENING_INTERVAL = 3;
    int _NIGHT_INTERVAL = 5;

    void Awake()
    {
    }

    //入力した値をupdateの秒数に応じて0~1で返す
    float ComputeOneFromZeroFromNumber(int number, float currentTime)
    {
        return 1 - ((number - currentTime) / number);
    }

    float _changeTimer = 0;
    bool eveningFromDaytime = true;
    bool evening = true;
    bool nightFromEvening = true;



    void DayTimerStart()
    {
        _currentTime += Time.deltaTime;
        _changeTimer += Time.deltaTime;

        int DAYTIME_SECONDS = _DAYTIME_INTERVAL;
        int EVENING_FROM_DAYTIME_SECONDS = DAYTIME_SECONDS + _EVENING_FROM_DAYTIME_INTERVAL;
        int EVENING_SECONDS = EVENING_FROM_DAYTIME_SECONDS + _EVENING_INTERVAL;
        int NIGHT_FROM_EVENING_SECONDS = EVENING_SECONDS + _NIGHT_FROM_EVENING_INTERVAL;
        int NIGHT_SECONDS = NIGHT_FROM_EVENING_SECONDS + _NIGHT_INTERVAL;



        if (_currentTime < DAYTIME_SECONDS)
        {
            Debug.Log("お昼");

        }
        else if (_currentTime >= DAYTIME_SECONDS && _currentTime < EVENING_FROM_DAYTIME_SECONDS)
        {
            Debug.Log("お昼から夕方");
            if (eveningFromDaytime)
            {
                eveningFromDaytime = false;
                _changeTimer = 0;
            }

            //0は昼の色~1は夕方の色で補完
            _dayLight.color = Color.Lerp(_dayTimeColor, _eveningColor, ComputeOneFromZeroFromNumber(_EVENING_FROM_DAYTIME_INTERVAL, _changeTimer));

        }
        else if (_currentTime >= EVENING_FROM_DAYTIME_SECONDS && _currentTime < EVENING_SECONDS)
        {
            Debug.Log("夕方");
            _dayLight.color = _eveningColor;
        }
        else if (_currentTime >= EVENING_SECONDS && _currentTime < NIGHT_FROM_EVENING_SECONDS)
        {
            Debug.Log("夕方から夜");
            if (nightFromEvening)
            {
                nightFromEvening = false;
                _changeTimer = 0;
            }
            //0は夕方の色~1は夜の色で補完
            //Debug.Log(ComputeOneFromZeroFromNumber(_NIGHT_FROM_EVENING_INTERVAL, _currentTime - 3));
            _dayLight.color = Color.Lerp(_eveningColor, _nightColor, ComputeOneFromZeroFromNumber(_NIGHT_FROM_EVENING_INTERVAL, _changeTimer));
        }
        else if (_currentTime >= NIGHT_FROM_EVENING_SECONDS && _currentTime < NIGHT_SECONDS)
        {
            Debug.Log("夜");
            _dayLight.color = _nightColor;
        }
    }

    void Update()
    {
        DayTimerStart();
        //else if (_currentTime >= NIGHT_SECONDS)
        //{
        //    _currentTime = 0;

        //}

        //Debug.Log(_currentTime);
    }

}
