using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class LoadingScreen : MonoBehaviourSingleton<LoadingScreen>
{
    [SerializeField] Renderer _RenderImage;
    RenderTexture _rendTex;
    [SerializeField] Camera _RendCamera;
    [SerializeField] RawImage _RawImage;

    [SerializeField] GameObject _LoadingCircle;
    [SerializeField] TextMeshProUGUI _loadingMainText;
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] GameObject _LoadingBar;
    [SerializeField] Image progressImage;
    [SerializeField] float _AnimSpeed;

    bool canClose;

    public event Action OnClose;
    public event Action OnOpen;
    Action _OnAnimStartFinish;

    bool _initialized;
    float _minActiveTime;

    bool _screenActive;
    bool _animationFinished;

    Coroutine _autoClose;

    private void Init()
    {
        if (!_initialized)
        {
            _rendTex = new RenderTexture(Screen.width, Screen.height, 24);
            _rendTex.Create();
            _RendCamera.targetTexture = _rendTex;
            Vector3 localScale = _RenderImage.transform.localScale;
            localScale.x = ((float)Screen.width / (float)Screen.height) * localScale.y;
            _RenderImage.transform.localScale = localScale;
            _RawImage.texture = _rendTex;
            _initialized = true;
        }
    }

    private void OnEnable()
    {
        _RendCamera.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _RendCamera.gameObject.SetActive(false);
    }

    public void Show(float minActiveTime = 0, Action onAnimationFinish = null,bool autoClose=true)
    {
        gameObject.SetActive(true);
        if (_screenActive)
        {
            OnOpen?.Invoke();
            _OnAnimStartFinish?.Invoke();
            _OnAnimStartFinish = onAnimationFinish;
            if (_animationFinished)
            {
                _OnAnimStartFinish?.Invoke();
                _OnAnimStartFinish = null;
            }
            if (autoClose)
            {
                StartAutoClose();
            }
            return;
        }
        Init();
        _LoadingBar.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);
        _OnAnimStartFinish = onAnimationFinish;
        canClose = false;
        OnOpen?.Invoke();
        _minActiveTime = minActiveTime;
        StopAllCoroutines();
        StartCoroutine(Coroutine_LoadingScreenShow());
        if (autoClose)
        {
            StartAutoClose();
        }
        _screenActive = true;
    }

    private void StartAutoClose()
    {
        if (_autoClose != null)
        {
            PersistentData.Instance.StopCoroutine(_autoClose);
        }
        _autoClose = PersistentData.Instance.StartCoroutine(Coroutine_AutoClose());
    }

    IEnumerator Coroutine_LoadingScreenShow()
    {
        _animationFinished = false;
        float lerp = 0;
        while (lerp < 1)
        {
            _RenderImage.material.SetFloat("_Disolve", 1-lerp);
            lerp += Time.deltaTime* _AnimSpeed;
            yield return null;
        }
        _RenderImage.material.SetFloat("_Disolve",0);
        _animationFinished = true;
        UpdateProgress(0);
        UpdateText("");
        StopAllCoroutines();
        StartCoroutine(CloseAfterMinimumActiveTimer(_minActiveTime));
        _OnAnimStartFinish?.Invoke();
        _OnAnimStartFinish = null;
    }

    IEnumerator Coroutine_LoadingScreenHide()
    {
        float lerp = 0;
        while (lerp < 1)
        {
            _RenderImage.material.SetFloat("_Disolve", lerp);
            lerp += Time.deltaTime* _AnimSpeed;
            yield return null;
        }
        _RenderImage.material.SetFloat("_Disolve", 1);
        gameObject.SetActive(false);
    }

    public void ElementsToggle(bool activate)
    {
        _loadingMainText.gameObject.SetActive(activate);
        _LoadingCircle.SetActive(activate);
        //_LoadingBar.gameObject.SetActive(activate);
        //loadingText.gameObject.SetActive(activate);
    }

    public void StartAnimFinish()
    {
        _OnAnimStartFinish?.Invoke();

        ElementsToggle(true);
        UpdateProgress(0);
        UpdateText("");
        StopAllCoroutines();
        StartCoroutine(CloseAfterMinimumActiveTimer(_minActiveTime));
    }

    public void Close()
    {
        if (_autoClose != null)
        {
            PersistentData.Instance.StopCoroutine(_autoClose);
        }
        _screenActive = false;
        canClose = true;
    }

    public void UpdateText(string msg)
    {
        loadingText.text = msg;
    }

    public void UpdateProgress(float value)
    {
        value = Mathf.Clamp01(value);
        progressImage.fillAmount = value;
    }

    private void OnCloseScreen()
    {
        OnClose?.Invoke();
        StopAllCoroutines();
        StartCoroutine(Coroutine_LoadingScreenHide());
    }

    IEnumerator Coroutine_AutoClose()
    {
        float time = 6;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        Close();
    }

    IEnumerator CloseAfterMinimumActiveTimer(float time)
    {
        float t = time;
        while (t > 0)
        {
            UpdateProgress(1 - t / time);
            t -= Time.deltaTime;
            yield return null;
        }
        while (!canClose)
        {
            yield return null;
        }
        OnCloseScreen();
    }
}
