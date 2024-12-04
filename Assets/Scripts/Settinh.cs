using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
using UnityEngine.UI;

public class Settinh : MonoBehaviour
{
    public Button policyButton;
    public Button termsButton;
    public Button shareApp;

    public AudioSource audioSource;

    public Slider musicVolume;
    public Button buttonMinus;
    public Button buttonPlus;

    [SerializeField] string _policyString;
    [SerializeField] string _termsString;
    private UniWebView webView;
    private void Start()
    {
        policyButton.onClick.AddListener(() => OpenWebView(_policyString));
        termsButton.onClick.AddListener(() => OpenWebView(_termsString));

        shareApp.onClick.AddListener(ShareApp);

        musicVolume.value = GameManager.InstanceGame.volumeSound;  // Устанавливаем текущее значение громкости на слайдер
        musicVolume.onValueChanged.AddListener(ChangeVolume);

        buttonMinus.onClick.AddListener(Minus);
        buttonPlus.onClick.AddListener(Plus);
    }
    public void Plus()
    {
        musicVolume.value += 0.1f;
        GameManager.InstanceGame.volumeSound = musicVolume.value;
        audioSource.volume = GameManager.InstanceGame.volumeSound;

    }
    public void Minus()
    {
        musicVolume.value -= 0.1f;
        GameManager.InstanceGame.volumeSound = musicVolume.value;
        audioSource.volume = GameManager.InstanceGame.volumeSound;
    }
    void ChangeVolume(float value = 1)
    {
        GameManager.InstanceGame.volumeSound = value;  // Меняем громкость аудио
        audioSource.volume = GameManager.InstanceGame.volumeSound;
    }
    void ShareApp()
    {
#if UNITY_IOS
        Device.RequestStoreReview();
#endif
    }
    void OpenWebView(string url)
    {
        webView = gameObject.AddComponent<UniWebView>();

        webView.EmbeddedToolbar.Show();
        webView.EmbeddedToolbar.SetPosition(UniWebViewToolbarPosition.Top);
        webView.EmbeddedToolbar.SetDoneButtonText("Close");
        webView.EmbeddedToolbar.SetButtonTextColor(Color.white);
        webView.EmbeddedToolbar.SetBackgroundColor(Color.red);
        webView.EmbeddedToolbar.HideNavigationButtons();
        webView.OnShouldClose += (view) => {
            webView = null;
            return true;
        };

        webView.Frame = new Rect(0, 0, Screen.width, Screen.height);

        webView.OnPageFinished += (view, statusCode, url) =>
        {
            if (statusCode == 200)
            {
                Debug.Log("WebView loaded successfully");
            }
            else
            {
                Debug.LogError("Failed to load WebView with status code: " + statusCode);
            }
        };

        webView.OnShouldClose += (view) =>
        {
            return true;
        };

        webView.Load(url);
        webView.Show();
        webView.EmbeddedToolbar.Show();
    }

    void OnDestroy()
    {
        if (webView != null)
        {
            webView.CleanCache();
            webView = null;
        }
    }
}
