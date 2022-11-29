using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoTxt : MonoBehaviour
{
    [SerializeField] private TMP_Text timeTxt, scoreTxt;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        Events.Time.AddListener(UpdateTime, true);
        Events.Score.AddListener(UpdateScore, true);
        Events.Energy.AddListener(UpdateEnergy, true);
    }

    private void UpdateTime(float time) => timeTxt.text = $"Time left: {time:f2}";
    private void UpdateScore(int score) => scoreTxt.text = $"Score: {score}";
    private void UpdateEnergy(float cur, float max)
    { 
        slider.value = cur;
    }

    private void OnDestroy()
    {
        Events.Time.RemoveListener(UpdateTime);
        Events.Score.RemoveListener(UpdateScore);
        Events.Energy.RemoveListener(UpdateEnergy);
    }
}
