using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanel : MonoBehaviour
{
    public Text TitleText;
    public Text DescriptionText;

    public GameObject PurchaseItemsHolder;

    public GameObject PurchaseItemUiPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show(PurchaseItemSet purchaseItemSet)
    {
        TitleText.text = purchaseItemSet.title;
        DescriptionText.text = purchaseItemSet.description;

        for (int i=0; i<PurchaseItemsHolder.transform.childCount; i++)
        {
            Destroy(PurchaseItemsHolder.transform.GetChild(i).gameObject);
        }

        for (int i=0; i<purchaseItemSet.purchaseItems.Count; i++)
        {
            PurchaseItem purchaseItem = purchaseItemSet.purchaseItems[i];

            GameObject purchaseItemUIGameObject = Instantiate(PurchaseItemUiPrefab, PurchaseItemsHolder.transform);

            PurchaseItemUI purchaseItemUi = purchaseItemUIGameObject.GetComponent<PurchaseItemUI>();
            purchaseItemUi.ItemNameText.text = purchaseItem.ItemName;
            purchaseItemUi.PurchaseButton.GetComponentInChildren<Text>().text = "$" + purchaseItem.Cost;

            purchaseItem.CheckPurchaseItemStatus.Invoke(purchaseItemUi);

            if (purchaseItem.Cost > GameManager.Instance.Money)
            {
                purchaseItemUi.Disable();
            }

            purchaseItemUi.PurchaseButton.onClick.AddListener(() =>
            {
                GameManager.Instance.OnMoneySpent(purchaseItem.Cost);
                purchaseItem.OnItemPurchased.Invoke();
                Close();
            });
        }

        GameManager.Instance.PauseTime();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        GameManager.Instance.ResumeTime();
        gameObject.SetActive(false);
    }
}
