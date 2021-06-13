using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;

    public Attribute[] attributes;

    private GameObject[] UIElements;
    private GameObject player;
    private TurnBasedPlayerMovement movement;
    private PlayerAttackHandler attacks;
    private bool inventoryIsOpen = true;

    public CharacterStats CharStats;

    private void Start()
    {
        UIElements = GameObject.FindGameObjectsWithTag("Inventory");
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<TurnBasedPlayerMovement>();
        attacks = player.GetComponent<PlayerAttackHandler>();
        CharStats = GetComponent<CharacterStats>();

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
            attributes[i].value.BaseValue = (int) CharStats.GetStat(attributes[i].type);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }

        ToggleInventory();
    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if(_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case interfaceType.Inventory:
                break;
            case interfaceType.Equipment:
                //print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute) 
                        { 
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                            CharStats.DecreaseStats(attributes[j].type, _slot.item.buffs[i].value);
                        }
                    }
                }

                break;
            case interfaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if(_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case interfaceType.Inventory:
                break;
            case interfaceType.Equipment:
                //print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if(attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                            CharStats.IncreaseStats(attributes[j].type, _slot.item.buffs[i].value);
                        }
                            
                    }
                }

                break;
            case interfaceType.Chest:
                break;
            default:
                break;
        }
    }

    private PlayerControls m_playerControls;
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if(item)
        {
            Item _item = new Item(item.item);
            if(inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }

    public void ToggleInventory()
    {
        inventoryIsOpen = !inventoryIsOpen;
        if(UIElements == null)
            UIElements = GameObject.FindGameObjectsWithTag("Inventory");
        foreach (var screen in UIElements)
        {
            var imageComp = screen.GetComponent<Image>();
            imageComp.enabled = inventoryIsOpen;

            foreach(var subelement in screen.GetComponentsInChildren<Image>())
            {
                subelement.enabled = inventoryIsOpen;
            }
            //screen.SetActive(!screen.activeSelf);
        }
        movement.ToggleMovementControls(!inventoryIsOpen);
        attacks.ToggleAttackControls(!inventoryIsOpen);
    }

    void OnEnable()
    {
        m_playerControls.Inventory.Enable();
    }

    void OnDisable()
    {
        m_playerControls.Inventory.Disable();
    }
    
    void Awake()
    {
        m_playerControls = new PlayerControls();
        m_playerControls.Inventory.SaveInventory.performed += ctx => inventory.Save();
        m_playerControls.Inventory.LoadInventory.performed += ctx => inventory.Load();
        m_playerControls.Inventory.SaveInventory.performed += ctx => equipment.Save();
        m_playerControls.Inventory.LoadInventory.performed += ctx => equipment.Load();
        m_playerControls.Inventory.OpenInventory.performed += ctx => ToggleInventory();
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public Player parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(Player _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }
    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}