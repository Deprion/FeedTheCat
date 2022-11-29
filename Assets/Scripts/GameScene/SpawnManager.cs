using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab, energyPrefab;
    [SerializeField] private Transform parent;

    private GameObject curFish, curEnergy;

    private float radius = 7;

    public static SpawnManager inst;

    private void Awake()
    {
        inst = this;

        SpawnFish();
        SpawnEnergy();
    }

    public void FishEat()
    {
        GameManager.inst.AddScore();
        Destroy(curFish);

        SpawnFish();
    }

    public void EnergyEat()
    {
        Destroy(curEnergy);

        SpawnEnergy();
    }

    public void DestroyAll()
    {
        Destroy(curFish);
        Destroy(curEnergy);
    }

    public void Restart()
    {
        DestroyAll();

        SpawnFish();
        SpawnEnergy();
    }

    private void SpawnFish()
    {
        var fish = Instantiate(fishPrefab, parent, false);
        fish.transform.localPosition = GetPos();
        curFish = fish;
    }

    private void SpawnEnergy()
    {
        var energy = Instantiate(energyPrefab, parent, false);
        energy.transform.localPosition = GetPos();
        curEnergy = energy;
    }

    private Vector2 GetPos()
    {
        Vector2 pos = new Vector2(Random.Range(-8, 9), Random.Range(-4, 4));

        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radius);

        foreach (var col in colliders)
        {
            if (col.CompareTag("Player")) return GetPos();
        }

        return pos;
    }
}
