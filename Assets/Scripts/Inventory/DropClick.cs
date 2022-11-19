using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropClick : MonoBehaviour, IPointerClickHandler
{
    public LevelControl pauseMenu;
    //public Inventory inventory;
    public int slotIndex;
    public bool isSlot;
    public bool isHot;

    public void OnPointerClick(PointerEventData eventData)
    {
        //print("clicked");

        //    if (eventData.button == PointerEventData.InputButton.Right)
        //    {
        //        if (pauseMenu.discarding == true)
        //        {
        //            //print("DiscarD");
        //            inventory.Drop(slotIndex);
        //        }
        //        else
        //        {

        //        }
        //    }

        //    if (eventData.button == PointerEventData.InputButton.Left)
        //    {




        //        if (!isSlot && !isHot)
        //        {
        //            //inventory.hotSlots[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);

        //            if (pauseMenu.selectedMain != slotIndex)
        //            {
        //                if (inventory.items[slotIndex] != null && pauseMenu.selectedSlot == -1)
        //                {

        //                    inventory.RewriteItemDescription(inventory.items[slotIndex].index);
        //                }

        //                if(inventory.items[slotIndex] == null)
        //                {

        //                    inventory.ClearItemDescription();
        //                }


        //                pauseMenu.selectedMain = slotIndex;
        //                inventory.selectItem = slotIndex;
        //                inventory.selectHot = -1;
        //                pauseMenu.CheckUse(slotIndex);
        //            }
        //            else
        //            {
        //                //pauseMenu.ResetGunDescription();
        //                pauseMenu.selectedMain = -1;
        //                inventory.selectItem = -1;
        //                inventory.selectHot = -1;
        //                pauseMenu.ResetUse();
        //            }


        //        }




        //        inventory.RefreshUI();
        //    }



    }



}
