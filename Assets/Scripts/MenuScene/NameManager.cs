using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    [SerializeField] private Button acceptName, close;
    [SerializeField] private TMP_InputField inp;

    private void Awake()
    {
        if (DataManager.instance.data.DisplayName == null)
        {
            close.gameObject.SetActive(false);
        }

        inp.text = DataManager.instance.data.DisplayName;

        close.onClick.AddListener(() => Destroy(gameObject));
        acceptName.onClick.AddListener(AcceptName);
    }

    private void AcceptName()
    {
        if (inp.text.Length >= 3 && inp.text != DataManager.instance.data.DisplayName)
        {
            Login.SetDisplayName(inp.text);
            DataManager.instance.SetName(inp.text);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        close.onClick.RemoveAllListeners();
        acceptName.onClick.RemoveAllListeners();
    }
}
