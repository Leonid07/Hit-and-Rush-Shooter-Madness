using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    public void Start()
    {
        buttonShopSwitch.onClick.AddListener(() =>
        {
            BuyAndTake();
        });
    }

    [Header("Маханика самого магазина")]
    public TMP_Text buttonText;
    public int price;
    public Button buttonShopSwitch;
    public Image buttonImage;

    [Header("Куплено")]
    public string isBuy_1 = "Select";
    public Sprite isBuySprite_1;

    [Header("Одето")]
    public string isBuy_2 = "It's used";
    public Sprite isBuySprite_2;

    [Header("Не куплено")]
    public string isBuy_3;
    public Sprite isBuySprite_3;

    public int isBuy = 0;
    public string idBuy;

    [Header("Какой скин будет выбран")]
    public int skeenID;

    // 0  не куплено
    // 1 куплено но не одето
    // 2 купленно и одето

    [Header("Переключатель для скинов оружия")]
    public bool isGun = false;

    private void Awake()
    {
        idBuy = gameObject.name;
        isBuy_3 = price.ToString();
        Check();
    }

    private void BuyAndTake()
    {
        if (isBuy == 0)
        {
            if (DataManger.InstanceData.Coin >= price)
            {
                DataManger.InstanceData.Coin -= price;
                DataManger.InstanceData.SaveCoint();
                DataManger.InstanceData.ApplyCoin();
                isBuy++;
                Check();

                if (isGun == false)
                {
                    PanelManager.InstancePanel.SaveButtonShop();
                }
                else
                {
                    PanelManager.InstancePanel.SaveButtonShopGun();
                }
                return;
            }
        }
        if (isBuy == 1)
        {
            if (isGun == false)
            {
                for (int i = 0; i < PanelManager.InstancePanel.buttonShops.Length; i++)
                {
                    if (PanelManager.InstancePanel.buttonShops[i].isBuy == 0)
                    {
                        continue;
                    }
                    if (PanelManager.InstancePanel.buttonShops[i].isBuy == 2)
                    {
                        PanelManager.InstancePanel.buttonShops[i].isBuy = 1;
                        PanelManager.InstancePanel.buttonShops[i].Check();
                    }
                }
            }
            else
            {
                for (int i = 0; i < PanelManager.InstancePanel.buttonShopsGun.Length; i++)
                {
                    if (PanelManager.InstancePanel.buttonShopsGun[i].isBuy == 0)
                    {
                        continue;
                    }
                    if (PanelManager.InstancePanel.buttonShopsGun[i].isBuy == 2)
                    {
                        PanelManager.InstancePanel.buttonShopsGun[i].isBuy = 1;
                        PanelManager.InstancePanel.buttonShopsGun[i].Check();
                    }
                }
            }

            isBuy = 2;
            Check();

            if (isGun == false)
            {
                PanelManager.InstancePanel.SaveButtonShop();
            }
            else
            {
                PanelManager.InstancePanel.SaveButtonShopGun();
            }
        }

    }

    public void Check()
    {
        switch (isBuy)
        {
            case 0:
                buttonText.text = isBuy_3;
                buttonImage.sprite = isBuySprite_3;
                break;
            case 1:
                buttonText.text = isBuy_1;
                buttonImage.sprite = isBuySprite_1;
                break;
            case 2:
                if (isGun == false)
                {
                    GameManager.InstanceGame.countSkeenPlayer = skeenID;
                    GameManager.InstanceGame.SaveCountSkeenPlayer();
                }
                else
                {
                    GameManager.InstanceGame.countSkeenGunPlayer = skeenID;
                    GameManager.InstanceGame.SaveCountSkeenGunPlayer();
                }

                buttonText.text = isBuy_2;
                buttonImage.sprite = isBuySprite_2;
                break;
        }
    }
}