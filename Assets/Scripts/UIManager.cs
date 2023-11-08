using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject         _interactionPanel;
    [SerializeField] private TextMeshProUGUI    _interactionMessage;
    [SerializeField] private Image[]            _inventorySlots;
    [SerializeField] private Image[]            _inventoryIcons;
    [SerializeField] private float              _unselectedAlpha;
    [SerializeField] private float              _selectedAlpha;

    void Start()
    {
        HideInteractionPanel();
        HideInventoryIcons();
        SelectInventorySlot(-1);
    }

    public void HideInteractionPanel()
    {
        _interactionPanel.SetActive(false);
    }

    public void ShowInteractionPanel(string message)
    {
        _interactionMessage.text = message;
        _interactionPanel.SetActive(true);
    }

    public int GetInventorySlotCount()
    {
        return _inventorySlots.Length;
    }

    public void HideInventoryIcons()
    {
        foreach (Image image in _inventoryIcons)
            image.enabled = false;
    }

    public void ShowInventoryIcon(int index, Sprite icon)
    {
        _inventoryIcons[index].sprite   = icon;
        _inventoryIcons[index].enabled  = true;
    }

    public void SelectInventorySlot(int index)
    {
        foreach (Image image in _inventorySlots)
        {
            Color color = image.color;
            color.a     = _unselectedAlpha;
            image.color = color;
        }

        if (index != -1)
        {
            Color color = _inventorySlots[index].color;
            color.a     = _selectedAlpha;

            _inventorySlots[index].color = color;
        }
    }
}
