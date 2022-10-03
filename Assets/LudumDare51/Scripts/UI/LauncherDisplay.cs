using UnityEngine;
using UnityEngine.UI;

public class LauncherDisplay : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image _image;

    [Header("Components")]
    [SerializeField] private Color _default = Color.white;
    [SerializeField] private Color _disabled = Color.gray;

    public void SetLauncherActive(bool active)
    {
        _image.color = active ? _default : _disabled;
    }

    public void DisplayLauncher(LauncherAsset asset)
    {
        _image.sprite = asset.IconSprite;
    }

    public void ClearLauncher()
    {
        _image.color = Color.clear;
    }
}
