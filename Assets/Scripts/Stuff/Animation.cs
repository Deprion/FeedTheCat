using UnityEngine;
using UnityEngine.UI;

public class Animation : MonoBehaviour
{
    [SerializeField] protected Sprite[] moveRight, moveLeft;
    [SerializeField] protected float timer;
    protected float leftTime = 0.15f;
    protected int index = 0;
    private Sprite[] cur;

    protected SpriteRenderer image;

    protected virtual void Start()
    {
        SetUp();
    }

    protected virtual void Update()
    {
        Anim();
    }

    public void ChangeDirLeft()
    {
        if (cur == moveLeft) return;
        cur = moveLeft;
        image.sprite = cur[0];
    }

    public void ChangeDirRight()
    {
        if (cur == moveRight) return;
        cur = moveRight;
        image.sprite = cur[0];
    }

    protected virtual void SetUp()
    {
        cur = moveRight;
        image = GetComponent<SpriteRenderer>();
        image.sprite = cur[index];
        leftTime = timer;
    }

    protected virtual void Anim()
    {
        leftTime -= Time.deltaTime;

        if (leftTime <= 0)
        {
            leftTime = timer;

            index = index + 1 >= cur.Length ? 0 : index + 1;

            image.sprite = cur[index];
        }
    }
}
