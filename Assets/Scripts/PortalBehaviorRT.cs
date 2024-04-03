using System;
using System.Collections;
using UnityEngine;

public enum PortalTransitionType
{
    scaling = 0,
    alpha
}

public class PortalBehaviorRT : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] Transform _portalPlane;
    [SerializeField] float _portalTargetScale = 0.6f;
    [SerializeField] RenderTexture _baseRenderTexture;
    [SerializeField] Camera _myCamera;
    [SerializeField] PortalTransitionType _transitionType = 0;
    [SerializeField] private float _scaleTransitionTime = 0.3f;
    [SerializeField] private float _fadeTransitionTime = 0.8f;

    private Vector3 _portalTargetSize;
    private bool _transitioning = false;
    private Coroutine _transitionCoroutine;

    private Renderer _portalRenderer;
    private Material _portalMaterial;

    private void Awake()
    {
        _portalRenderer = _portalPlane.GetComponent<Renderer>();

        if (_portalRenderer != null)
            _portalMaterial = _portalRenderer.material;
    }

    private void Start()
    {
        _particleSystem.Stop();
        _audioSource.Stop();
        _portalPlane.gameObject.SetActive(false);
        _portalTargetSize = Vector3.one * _portalTargetScale;
        _myCamera.targetTexture = null;

        if (_transitionType == PortalTransitionType.alpha)
        {
            _portalPlane.localScale = _portalTargetSize;
            _portalMaterial.color = new Color(_portalMaterial.color.r, _portalMaterial.color.g, _portalMaterial.color.b, 0f);
        }
        else
        {
            _portalPlane.localScale = Vector3.zero;
            _portalMaterial.color = new Color(_portalMaterial.color.r, _portalMaterial.color.g, _portalMaterial.color.b, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnterPortalZone(other);
    }

    private void OnTriggerExit(Collider other)
    {
        ExitPortalZone(other);
    }

    private void EnterPortalZone(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _particleSystem.Play();
            _audioSource.Play();

            if (_transitionType == PortalTransitionType.scaling)
                StartScalingPortal();
            else
                StartFadingPortal(0f, 1f);
        }
    }


    public void ExitPortalZone(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _particleSystem.Stop();
            _audioSource.Stop();

            if (_transitionCoroutine != null)
            {
                StopCoroutine(_transitionCoroutine);
            }

            if (_transitionType == PortalTransitionType.scaling)
                _transitionCoroutine = StartCoroutine(ScalePortal(Vector3.zero, _scaleTransitionTime));
            else
                _transitionCoroutine = StartCoroutine(FadePortal(1f, 0f, _fadeTransitionTime));
        }
    }

    private void StartScalingPortal()
    {
        if (_transitioning && _portalPlane.localScale != _portalTargetSize)
        {
            StopCoroutine(_transitionCoroutine);
            _transitionCoroutine = StartCoroutine(ScalePortal(_portalTargetSize, _scaleTransitionTime));
        }
        else if (!_transitioning)
        {
            _transitionCoroutine = StartCoroutine(ScalePortal(_portalTargetSize, _scaleTransitionTime));
        }
    }

    private void StartFadingPortal(float startAlpha, float targetAlpha)
    {
        if (_transitioning && _portalMaterial.color.a != targetAlpha)
        {
            StopCoroutine(_transitionCoroutine);
            _transitionCoroutine = StartCoroutine(FadePortal(startAlpha, targetAlpha, _fadeTransitionTime));
        }
        else if (!_transitioning)
        {
            _transitionCoroutine = StartCoroutine(FadePortal(startAlpha, targetAlpha, _fadeTransitionTime));
        }
    }

    private IEnumerator ScalePortal(Vector3 targetSize, float time)
    {
        _transitioning = true;
        Vector3 startSize = _portalPlane.localScale;
        float elapsedTime = 0f;

        if (targetSize != Vector3.zero)
        {
            _portalPlane.gameObject.SetActive(true);
            _myCamera.targetTexture = _baseRenderTexture;
        }

        while (elapsedTime < time)
        {
            _portalPlane.localScale = Vector3.Lerp(startSize, targetSize, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _portalPlane.localScale = targetSize;
        if (targetSize == Vector3.zero)
        {
            _portalPlane.gameObject.SetActive(false);
            _myCamera.targetTexture = null;
        }
        _transitioning = false;
    }

    private IEnumerator FadePortal(float startAlpha, float targetAlpha, float time)
    {       
        if (_portalRenderer == null)
        {
            yield break;
        }

        _transitioning = true;
        float elapsedTime = 0f;
        _portalPlane.gameObject.SetActive(true);
        _myCamera.targetTexture = _baseRenderTexture;

        Color materialColor = _portalRenderer.material.color;

        while (elapsedTime < time)
        {
            materialColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / time);
            _portalRenderer.material.color = materialColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _portalRenderer.material.color = new Color(_portalRenderer.material.color.r, _portalRenderer.material.color.g, _portalRenderer.material.color.b, targetAlpha);

        if (targetAlpha == 0f)
        {
            _portalPlane.gameObject.SetActive(false);
            _myCamera.targetTexture = null;
        }
        _transitioning = false;
    }

    public Camera GetCamera()
    {
        return _myCamera;
    }
}
