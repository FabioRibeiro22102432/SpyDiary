using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;

    private PlayerInteraction   _playerInteraction;
    private List<Interactive>   _inventory;
    private int                 _selectedSlot;

    void Start()
    {
        _playerInteraction  = GetComponent<PlayerInteraction>();
        _inventory          = new List<Interactive>();
        _selectedSlot       = -1;
    }

    public void Add(Interactive item)
    {
        _inventory.Add(item);

        _uiManager.ShowInventoryIcon(_inventory.Count - 1, item.inventoryIcon);

        if (_inventory.Count == 1)
            SelectInventorySlot(0);
    }

    public void Remove(Interactive item)
    {
        _inventory.Remove(item);

        _uiManager.HideInventoryIcons();

        for (int i = 0; i < _inventory.Count; ++i)
            _uiManager.ShowInventoryIcon(i, _inventory[i].inventoryIcon);

        if (_selectedSlot == _inventory.Count)
            SelectInventorySlot(_selectedSlot - 1);
    }

    public bool Contains(Interactive item)
    {
        return _inventory.Contains(item);
    }

    private void SelectInventorySlot(int index)
    {
        if (index != _selectedSlot)
        {
            _selectedSlot = index;
            _uiManager.SelectInventorySlot(index);
            _playerInteraction.ForceRefreshCurrentInteractive();
        }
    }

    public string GetSelectedInteractionMessage()
    {
        return _selectedSlot != -1 ? _inventory[_selectedSlot].GetInteractionMessage() : null;
    }

    public bool IsSelected(Interactive item)
    {
        return _selectedSlot != -1 && _inventory[_selectedSlot] == item;
    }

    public Interactive GetSelected()
    {
        return _selectedSlot != -1 ? _inventory[_selectedSlot] : null;
    }

    void Update()
    {
        CheckForPlayerSlotSelection();
    }

    private void CheckForPlayerSlotSelection()
    {
        for (int i = 0; i < _inventory.Count; ++i)
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                SelectInventorySlot(i);
    }
}
