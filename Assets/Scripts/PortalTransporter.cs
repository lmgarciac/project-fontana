using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PortalTransporter : MonoBehaviour
{
    [SerializeField] private PortalBehaviorRT _targetPortal;

    [SerializeField] private float _effectIntensity = 5f;
    [SerializeField] private float _transitionTime = 2f;
    [SerializeField] private float _waitTime = 2f;


    private ChromaticAberration effect;
    private float initialIntensity;
    private PostProcessVolume _mainVolume;

    private void Awake()
    {
        _mainVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
    }

    private void Start()
    {
        //if (_mainVolume == null) { return; }

        //_mainVolume.profile.TryGetSettings(out effect);

        //if (effect != null)
        //{
        //    initialIntensity = effect.intensity;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TransportToDestination(other));
        }
    }

    private IEnumerator TransportToDestination(Collider other)
    {
        Transform cameraTransform = _targetPortal.GetCamera().transform;

        CharacterController playerController = other.gameObject.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playerController.enabled = false;

            //yield return StartCoroutine(TransitionEffect());

            other.gameObject.transform.position = cameraTransform.position + cameraTransform.forward;
            other.gameObject.transform.rotation = Quaternion.Euler(0f, cameraTransform.rotation.eulerAngles.y, 0f);

            //effect.intensity.value = initialIntensity;
            playerController.enabled = true;

            _targetPortal.ExitPortalZone(other);
        }

        yield break;
    }

    private IEnumerator TransitionEffect()
    {
        if (effect == null)
            yield break;

        float elapsedTime = 0f;

        while (elapsedTime < _transitionTime)
        {
            effect.intensity.value = Mathf.Lerp(initialIntensity, _effectIntensity, elapsedTime / _transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_waitTime);

        elapsedTime = 0f;

        while (elapsedTime < _transitionTime)
        {
            effect.intensity.value = Mathf.Lerp(_effectIntensity, 0f, elapsedTime / _transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
