using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float startHour;
    [SerializeField] private Light sun;
    [SerializeField] private float sunriseHour;
    [SerializeField] private float sunsetHour;
    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;
    [SerializeField] private AnimationCurve lightChangeCurve;
    [SerializeField] private float maxSunLightIntensity;
    [SerializeField] private Light moonLight;
    [SerializeField] private float MaxMoonLightIntensity;

    private DateTime currentTime;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;
    // [SerializeField] private Material skybox;
    // private static readonly int Rotation = Shader.PropertyToID("_Rotation");
    // private static readonly int Exposure = Shader.PropertyToID("_Exposure");

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        SunRotation();
        UpdateLightSettings();
    }

    private void UpdateLightSettings(){
        float dotProdcut = Vector3.Dot(sun.transform.forward, Vector3.down);
        sun.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProdcut));
        moonLight.intensity = Mathf.Lerp(MaxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProdcut));

        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProdcut));
    }

    private void UpdateTime(){
        currentTime = currentTime.AddSeconds(timeMultiplier * Time.deltaTime);
        // skybox.SetFloat(Rotation, currentTime.Day * 3);
        // skybox.SetFloat(Exposure, Mathf.Clamp(Mathf.Sin(currentTime.Day * 3), 0.3f, 1f));
    }

    private void SunRotation(){
        float SunAngle;
        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime){
            TimeSpan riseToSetDuration = TimeDiff(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = TimeDiff(sunriseTime, currentTime.TimeOfDay);

            double percent = timeSinceSunrise.TotalMinutes / riseToSetDuration.TotalMinutes;

            SunAngle = Mathf.Lerp(0, 180, (float) percent);
        } else {
            TimeSpan SetToRiseDuration = TimeDiff(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = TimeDiff(sunsetTime, currentTime.TimeOfDay);

            double percent = timeSinceSunset.TotalMinutes / SetToRiseDuration.TotalMinutes;

            SunAngle = Mathf.Lerp(0, 180, (float) percent);
        }

        sun.transform.rotation = Quaternion.AngleAxis(SunAngle, Vector3.right);
    }

    private TimeSpan TimeDiff(TimeSpan from, TimeSpan to){
        TimeSpan difference = to - from;

        if(difference.TotalSeconds < 0){
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
}
