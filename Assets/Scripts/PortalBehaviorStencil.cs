using System.Collections;
using UnityEngine;

public class PortalBehaviorStencil : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] Transform _seeThroughSphere;

    private Vector3 _seeThroughSphereSize = new Vector3(1.3f, 2.3f, 0.1f);
    private float _transitionTime = 1.0f;
    private bool _transitioning = false;
    private Coroutine _scaleCoroutine;

    private void Start()
    {
        _particleSystem.Stop();
        _audioSource.Stop();
        _seeThroughSphere.gameObject.SetActive(false);
        _seeThroughSphere.localScale = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _particleSystem.Play();
            _audioSource.Play();
            if (_transitioning && _seeThroughSphere.localScale != _seeThroughSphereSize)
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
        Vector3 startSize = _seeThroughSphere.localScale;
        float elapsedTime = 0f;

        if (targetSize != Vector3.zero)
        {
            _seeThroughSphere.gameObject.SetActive(true);
        }

        while (elapsedTime < time)
        {
            _seeThroughSphere.localScale = Vector3.Lerp(startSize, targetSize, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _seeThroughSphere.localScale = targetSize;
        if (targetSize == Vector3.zero)
        {
            _seeThroughSphere.gameObject.SetActive(false);
        }
        _transitioning = false;
    }
}
