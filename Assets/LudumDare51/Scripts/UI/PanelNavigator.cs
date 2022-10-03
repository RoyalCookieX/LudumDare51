using System.Collections.Generic;
using UnityEngine;

public class PanelNavigator : MonoBehaviour
{
    public int ActivePanelCount => _panelIndices != null ? _panelIndices.Count : 0;
    public int ActivePanelIndex => _panelIndices != null ? _panelIndices.Peek() : 0;

    [Header("Components")]
    [SerializeField] private List<GameObject> _panels;

    private Stack<int> _panelIndices = new Stack<int>();

    public void PushPanelIndex(int index)
    {
        if (!IsValidPanel(index))
            return;

        _panelIndices.Push(index);

        HideAllPanels();
        _panels[index].SetActive(true);
    }

    public void PopPanelIndex()
    {
        if (_panelIndices.Count <= 1)
            return;

        _panelIndices.Pop();
        int index = _panelIndices.Peek();

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
