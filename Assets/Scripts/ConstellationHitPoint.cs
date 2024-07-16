using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConstellationHitPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering = false;

    [SerializeField]
    private ParticleSystem particleEffect;

    [SerializeField]
    private Image image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    public bool IsHovering()
    {
        return isHovering;
    }

    public void ActivatePoint()
    {
        if (particleEffect != null)
        {
            particleEffect.Play();
        }

        if (image != null)
        {
            image.enabled = false;
        }
    }
}
