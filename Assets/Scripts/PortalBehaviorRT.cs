using System.Collections;
using UnityEngine;

public class PortalBehaviorRT : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] Transform _seeThroughPlane;
    [SerializeField] float _seeThroughPlaneSize = 0.6f;

    private Vector3 _seeThroughSphereSize;
    private float _transitionTime = 0.3f;
    private bool _transitioning = false;
    private Coroutine _scaleCoroutine;

    private void Start()
    {
        _particleSystem.Stop();
        _audioSource.Stop();
        _seeThroughPlane.gameObject.SetActive(false);
        _seeThroughPlane.localScale = Vector3.zero;
        _seeThroughSphereSize = Vector3.one * _seeThroughPlaneSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _particleSystem.Play();
            _audioSource.Play();
            if (_transitioning && _seeThroughPlane.localScale != _seeThroughSphereSize)
            {
                StopCoroutine(_scaleCoroutine);
                _scaleCoroutine = StartCoroutine(ScaleSphere(_seeThroughSphereSize, _transitionTime));
            }
            else if (!_transitioning)
            {
                _scaleCoroutine = StartCoroutine(ScaleSphere(_seeThroughSphereSize, _transitionTime));
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _particleSystem.Stop();
            _audioSource.Stop();
            if (_scaleCoroutine != null)
            {
                StopCoroutine(_scaleCoroutine);
            }
            _scaleCoroutine = StartCoroutine(ScaleSphere(Vector3.zero, _transitionTime));
        }
    }

    private IEnumerator ScaleSphere(Vector3 targetSize, float time)
    {
        _transitioning = true;
        Vector3 startSize = _seeThroughPlane.localScale;
        float elapsedTime = 0f;

        if (targetSize != Vector3.zero)
        {
            _seeThroughPlane.gameObject.SetActive(true);
        }

        while (elapsedTime < time)
        {
            _seeThroughPlane.localScale = Vector3.Lerp(startSize, targetSize, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _seeThroughPlane.localScale = targetSize;
        if (targetSize == Vector3.zero)
        {
            _seeThroughPlane.gameObject.SetActive(false);
        }
        _transitioning = false;
    }
}
