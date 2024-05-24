using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    private Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        GlobalManager.Instance.FinishRestoration += OnFinishRestoration;
    }

    private void OnFinishRestoration(int paintingID) //Check if this ID is really always necessary
    {
        StartCoroutine(StartFade());
    }

    private IEnumerator StartFade()
    {
        myAnimator.Play("FadeIn");
        yield return new WaitForSeconds(2); //So the screen is black for an extra second
        myAnimator.Play("FadeOut");
    }

    private void OnDestroy()
    {
        GlobalManager.Instance.FinishRestoration -= OnFinishRestoration;
    }
}
