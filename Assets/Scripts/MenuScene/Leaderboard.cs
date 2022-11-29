using PlayFab.ClientModels;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject prefab, popUp;
    [SerializeField] private Transform parent;
    [SerializeField] private Button resetBtn;

    private LeadPlayfab lead;

    private WaitForSeconds waitFor = new WaitForSeconds(5);

    private bool request = false;

    private void Awake()
    {
        lead = GetComponent<LeadPlayfab>();
        LeadPlayfab.Result.AddListener(UpdateLead, true);
        resetBtn.onClick.AddListener(delegate
        {
            request = true;
            lead.UpdateLead();
            StartCoroutine(WaitBtn());
        });
    }

    private void UpdateLead(GetLeaderboardResult result)
    {
        int count = parent.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }

        foreach (var res in result.Leaderboard)
        {
            var obj = Instantiate(prefab, parent, false);
            obj.GetComponent<Entry>().SetData(res.DisplayName, res.StatValue.ToString());
        }

        if (request)
            popUp.SetActive(true);
    }

    private IEnumerator WaitBtn()
    {
        resetBtn.interactable = false;

        yield return waitFor;

        popUp.SetActive(false);

        resetBtn.interactable = true;

        request = false;
    }

    private void OnEnable()
    {
        if (resetBtn.interactable == false)
        {
            popUp.SetActive(false);
            StartCoroutine(WaitBtn());
        }
    }

    private void OnDestroy()
    {
        resetBtn.onClick.RemoveAllListeners();
        LeadPlayfab.Result.RemoveListener(UpdateLead);
    }
}
