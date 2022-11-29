using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class LeadPlayfab : MonoBehaviour
{
    public static SimpleEvent<GetLeaderboardResult> Result = new SimpleEvent<GetLeaderboardResult>();

    public void UpdateLead()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "HighScore",
            StartPosition = 0,
            MaxResultsCount = 8
        }, 
        result => 
        {
            Result.Invoke(result);
        },
        error => Debug.LogError(error.GenerateErrorReport()));
    }

    public static void SubmitScore(int value)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate {
                StatisticName = "HighScore",
                Value = value
            }
        }
        }, 
        result => { }, 
        error => Debug.LogError(error.GenerateErrorReport()));
    }
}
