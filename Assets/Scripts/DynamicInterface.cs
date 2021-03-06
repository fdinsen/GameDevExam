using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public GameObject inventoryPrefab;

    //Start koordinaten for x-aksen til den første item
    public int X_START;
    //Start koordinaten for y-aksen til den første item
    public int Y_START;
    //Distancen mellem hver item på x-aksen
    public int X_SPACE_BETWEEN_ITEM;
    //Antallet af items der skal være på hver kolonne.
    public int NUMBER_OF_COLUMN;
    //Distancen mellem hver item på y-aksen
    public int Y_SPACE_BETWEEN_ITEM;
    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
            inventory.GetSlots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }

    public void OnClick(GameObject obj)
    {
        InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
        if (mouseHoverSlotData.ItemObject == null)
            return;
        if (mouseHoverSlotData.ItemObject.type == ItemType.HealthPotion)
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            var stats = playerObj.GetComponent<Player>();
            if (stats.CharStats.characterDefinition.currentHealth == stats.CharStats.characterDefinition.maxHealth)
                return;

            stats.Heal(50);
            if (mouseHoverSlotData.amount == 1)
            {
                slotsOnInterface[obj].RemoveItem();
            }
            else
            {
                slotsOnInterface[obj].DetractAmount(1);
            }
            return;
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
