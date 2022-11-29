using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class Login : MonoBehaviour
{
    private static string playFabId;
    private static string sessionTicket;
    public static string PlayFabId { get { return playFabId; } }
    public static string SessionTicket { get { return sessionTicket; } }

    public GetPlayerCombinedInfoRequestParams InfoRequestParams;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        SilentAuth();
    }

    private void SilentAuth()
    {
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = InfoRequestParams
        },
        (result) =>
        {
            playFabId = result.PlayFabId;
            sessionTicket = result.SessionTicket;
        },
        (error) =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    public static void SetDisplayName(string displayName)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName
        },
        (result) => 
        {
            DataManager.instance.data.DisplayName = result.DisplayName;
        },
        (error) =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}
