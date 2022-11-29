using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject changeNamePrefab;
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private Button playBtn, changeNameBtn;

    private void Awake()
    {
        if (DataManager.instance.data.DisplayName == null) InitChange();

        scoreTxt.text = $"Highscore: {DataManager.instance.data.Highscore}";

        playBtn.onClick.AddListener(()=> SceneManager.LoadScene("GameScene"));
        changeNameBtn.onClick.AddListener(InitChange);
    }

    private void InitChange() => Instantiate(changeNamePrefab, parent, false);

    private void OnDestroy()
    {
        playBtn.onClick.RemoveAllListeners();
        changeNameBtn.onClick.RemoveAllListeners();
    }
}
