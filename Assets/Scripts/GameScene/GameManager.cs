using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject player;
    private float timeLeft = 20;
    private int score = 0;
    private bool dead = false;

    public static GameManager inst;

    private GameObject playerObj;

    private void Awake()
    {
        inst = this;

        Events.Score.Invoke(score);
        Events.Time.Invoke(timeLeft);
        Events.Dead.AddListener(Dead);
    }

    private void Start()
    {
        playerObj = Instantiate(player, parent, false);
    }

    void Update()
    {
        if (dead) return;

        if (timeLeft <= 0)
        {
            TimeRunsOut();
            return;
        }

        timeLeft -= Time.deltaTime;
        Events.Time.Invoke(timeLeft);
    }

    private void Dead() => dead = true;

    public void Restart()
    {
        SpawnManager.inst.Restart();
        Destroy(playerObj);

        playerObj = Instantiate(player, parent, false);
        score = 0;
        timeLeft = 20;
        dead = false;

        Events.Score.Invoke(score);
        Events.Time.Invoke(timeLeft);
    }

    public void AddScore()
    {
        score++;

        Events.Score.Invoke(score);
    }

    private void TimeRunsOut()
    {
        Events.TimeOut.Invoke();

        DataManager.instance.SetScore(score);
    }
    private void OnDestroy()
    {
        Events.Dead.RemoveListener(Dead);
    }
}
