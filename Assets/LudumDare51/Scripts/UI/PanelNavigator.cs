using System.Collections.Generic;
using UnityEngine;

public class PanelNavigator : MonoBehaviour
{
    public int ActivePanelCount => _panelIndices != null ? _panelIndices.Count : 0;
    public int ActivePanelIndex => _panelIndices != null ? _panelIndices.Peek() : 0;
    public bool NavigationEnabled { get => _navigationEnabled; set => _navigationEnabled = value; }

    [Header("Components")]
    [SerializeField] private List<GameObject> _panels;

    [Header("Properties")]
    [SerializeField] private bool _navigationEnabled = true;

    private Stack<int> _panelIndices = new Stack<int>();

    public void PushPanelIndex(int index)
    {
        if (!_navigationEnabled || !IsValidPanel(index))
            return;

        _panelIndices.Push(index);

        HideAllPanels();
        _panels[index].SetActive(true);
    }

    public void PopPanelIndex()
    {
        if (!_navigationEnabled || _panelIndices.Count <= 1)
            return;

        _panelIndices.Pop();
        int index = _panelIndices.Peek();

        HideAllPanels();
        _panels[index].SetActive(true);
    }

    public void StartAtPanelIndex(int index)
    {
        if (!_navigationEnabled || !IsValidPanel(index))
            return;

        _panelIndices.Clear();
        _panelIndices.Push(index);
        
        HideAllPanels();
        _panels[index].SetActive(true);
    }

    private void HideAllPanels()
    {
        foreach (GameObject panel in _panels)
            panel.SetActive(false);
    }

    private bool IsValidPanel(int index) => index >= 0 && index < _panels.Count && _panels[index];
}
