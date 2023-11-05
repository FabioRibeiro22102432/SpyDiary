using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private TextMeshProUGUI _interactionMessage;
    [SerializeField] private Image[] _inventorySlots;
    [SerializeField] private Image[] _inventoryItems;
    [SerializeField] private float _NotSelectedItem;
    [SerializeField] private float _selectedItem;

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
        foreach (Image image in _inventoryItems)
            image.enabled = false;
    }

    public void ShowInventoryIcon(int index, Sprite icon)
    {
        _inventoryItems[index].sprite = icon;
        _inventoryItems[index].enabled = true;
    }

    public void SelectInventorySlot(int index)
    {
        foreach (Image image in _inventorySlots)
        {
            Color color = image.color;
            color.a = _NotSelectedItem;
            image.color = color;
        }

        if (index != -1)
        {
            Color color = _inventorySlots[index].color;
            color.a = _selectedItem;

            _inventorySlots[index].color = color;
        }
    }
}
