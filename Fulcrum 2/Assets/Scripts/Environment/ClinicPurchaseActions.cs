using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClinicPurchaseActions : MonoBehaviour
{
    public float checkUpRiskReduction = 50f;

    public void OnCondomPurchased()
    {
        GameManager.Instance.OnCondomPurchased();
    }

    public void OnCheckUpPurchased()
    {
        GameManager.Instance.ModifyStats(0, 0, -checkUpRiskReduction);
    }

    public void CheckCondomPurchaseStatus(PurchaseItemUI purchaseItemUi)
    {
        if (GameManager.Instance.HasCondom)
        {
            purchaseItemUi.Disable();
        }
    }

    public void CheckCheckUpPurchaseStatus(PurchaseItemUI purchaseItemUi)
    {
        if (GameManager.Instance.Risk <= 0)
        {
            purchaseItemUi.Disable();
        }
    }
}
