using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text statusTxt;
    [SerializeField] private Button homeBtn, restartBtn;
    [SerializeField] private Image cat;
    [SerializeField] private Sprite[] cats;

    private int score;

    private void Awake()
    {
        Events.Dead.AddListener(Lose);
        Events.TimeOut.AddListener(Win);
        Events.Score.AddListener(ScoreSet);

        homeBtn.onClick.AddListener(() => SceneManager.LoadScene("MenuScene"));
        restartBtn.onClick.AddListener(delegate
        {
            panel.SetActive(false);
            GameManager.inst.Restart();
        });
    }

    public void Win()
    {
        panel.SetActive(true);
        if (score > 0)
        {
            statusTxt.text = "The cat is fed";
            cat.sprite = cats[0];
        }
        else
        {
            Lose();
        }
    }

    private void Lose()
    {
        panel.SetActive(true);
        statusTxt.text = "The cat is not fed";
        cat.sprite = cats[1];
    }

    private void ScoreSet(int scr) => score = scr;

    private void OnDestroy()
    {
        Events.Dead.RemoveListener(Lose);
        Events.TimeOut.RemoveListener(Win);
        Events.Score.RemoveListener(ScoreSet);
        homeBtn.onClick.RemoveAllListeners();
        restartBtn.onClick.RemoveAllListeners();
    }
}
