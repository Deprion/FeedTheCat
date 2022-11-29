using TMPro;
using UnityEngine;

public class Entry : MonoBehaviour
{
    [SerializeField] private TMP_Text nameTxt, scoreTxt;

    public void SetData(string name, string score)
    {
        nameTxt.text = name;
        scoreTxt.text = score;
    }
}
