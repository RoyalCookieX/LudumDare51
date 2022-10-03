using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _text;

    public void DisplayInt(int value)
    {
        _text.text = $"{value}";
    }
}
