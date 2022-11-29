using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float curEnergy, maxEnergy;

    private void Awake()
    {
        maxEnergy = 100;
        curEnergy = maxEnergy;

        Events.TimeOut.AddListener(TimeOut);
    }

    private void Update()
    {
        if (curEnergy <= 0)
        {
            Events.Dead.Invoke();
            TimeOut();
            return;
        }

        curEnergy -= 14 * Time.deltaTime;

        Events.Energy.Invoke(curEnergy, maxEnergy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            SpawnManager.inst.FishEat();
        }
        if (collision.CompareTag("Energy"))
        {
            curEnergy = curEnergy + 50 > 100 ? 100 : curEnergy + 50;
            Events.Energy.Invoke(curEnergy, maxEnergy);
            SpawnManager.inst.EnergyEat();
        }
    }

    public void SubEnergy(int value)
    {
        curEnergy -= value;
        Events.Energy.Invoke(curEnergy, maxEnergy);
    }

    private void TimeOut()
    {
        Destroy(GetComponent<PlayerMovement>());
        Destroy(GetComponent<Animation>());
        Destroy(this);
    }

    private void OnDestroy()
    {
        Events.TimeOut.RemoveListener(TimeOut);
    }
}
