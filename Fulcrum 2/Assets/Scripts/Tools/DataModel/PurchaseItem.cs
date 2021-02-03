using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

[Serializable]
public class PurchaseItem
{
    public string ItemName;
    public float Cost;
    public ItemStatusCheckEvent CheckPurchaseItemStatus;
    public UnityEvent OnItemPurchased;

    [Serializable]
    public class ItemStatusCheckEvent : UnityEvent<PurchaseItemUI>
    {

    }
}