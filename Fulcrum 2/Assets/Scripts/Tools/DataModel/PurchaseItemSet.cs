using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class PurchaseItemSet
{
    public string title = "Shop";
    public string description = "";
    public List<PurchaseItem> purchaseItems;
}