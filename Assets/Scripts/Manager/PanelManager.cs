using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager InstancePanel { get; private set; }

    private void Awake()
    {
        if (InstancePanel != null && InstancePanel != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstancePanel = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Button buttonLevel;

    [Header("Окна")]
    public Button buttonSetting;
    public Button buttonShop;
    public GameObject panelShop;
    public GameObject panelSetting;

    [Space(10)]
    public Button[] buttonClose;
    public GameObject[] panel;

    public void Start()
    {
        buttonLevel.onClick.AddListener(()=> { LoadLevel(); CanvasManager.InstanceMainCanvas.gameObject.SetActive(false); });

        buttonSetting.onClick.AddListener(() => { OpenPanel(panelSetting); });
        buttonShop.onClick.AddListener(() => { OpenPanel(panelShop); LoadButtonShop(); });

        for (int i = 0; i < buttonClose.Length; i++)
        {
            int count = i;
            buttonClose[count].onClick.AddListener(() => { ClosePanel(panel[count]); });
        }

        buttonLeft.onClick.AddListener(LeftButton);
        buttonRight.onClick.AddListener(RightButton);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(DataManger.InstanceData.countLevel);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    [Header("Настройки панили переключения в магазине")]
    public Button buttonLeft;
    public Button buttonRight;

    public Sprite spriteStandart;
    public Sprite spriteNew;

    public Image buttonLeftImage;
    public Image buttonRightImage;

    public GameObject panelCharacter;
    public GameObject panelShop_;

    public void LeftButton()
    {
        buttonRightImage.sprite = spriteNew;
        buttonLeftImage.sprite = spriteStandart;

        panelCharacter.SetActive(true);
        panelShop_.SetActive(false);
    }
    public void RightButton()
    {
        buttonRightImage.sprite = spriteStandart;
        buttonLeftImage.sprite = spriteNew;
        panelCharacter.SetActive(false);
        panelShop_.SetActive(true);
        LoadButtonShopGun();
    }

    public ButtonShop[] buttonShops;
    public ButtonShop[] buttonShopsGun;

    public void LoadButtonShop()
    {
        for (int i = 0; i < buttonShops.Length; i++)
        {
            if (PlayerPrefs.HasKey(buttonShops[i].idBuy))
            {
                buttonShops[i].isBuy = PlayerPrefs.GetInt(buttonShops[i].idBuy);
                buttonShops[i].Check();
            }
        }
    }
    public void LoadButtonShopGun()
    {
        for (int i = 0; i < buttonShopsGun.Length; i++)
        {
            if (PlayerPrefs.HasKey(buttonShopsGun[i].idBuy))
            {
                buttonShopsGun[i].isBuy = PlayerPrefs.GetInt(buttonShopsGun[i].idBuy);
                buttonShopsGun[i].Check();
            }
        }
    }

    public void SaveButtonShop()
    {
        for (int i = 0; i < buttonShops.Length; i++)
        {
            PlayerPrefs.SetInt(buttonShops[i].idBuy, buttonShops[i].isBuy);
        }
        PlayerPrefs.Save();
    }
    public void SaveButtonShopGun()
    {
        for (int i = 0; i < buttonShopsGun.Length; i++)
        {
            PlayerPrefs.SetInt(buttonShopsGun[i].idBuy, buttonShopsGun[i].isBuy);
        }
        PlayerPrefs.Save();
    }
}