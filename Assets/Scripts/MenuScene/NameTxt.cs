using TMPro;
using UnityEngine;

public class NameTxt : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private void Awake()
    {
        Events.Name.AddListener(ChangeName, true);
    }

    private void ChangeName(string name) => text.text = name;
}
