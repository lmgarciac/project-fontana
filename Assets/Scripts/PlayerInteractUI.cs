using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private TextMeshProUGUI interactableObjectPrompt;

    // Update is called once per frame
    void Update()
    {
        if (playerInteraction.GetInteractableObject() != null)
        {
            ShowUI(playerInteraction.GetInteractableObject());
        }
        else
        {
            HideUI();
        }
    }

    private void ShowUI(IInteractable interactable)
    {
        GameObject objectInHand = playerInteraction.ObjectInHand;
        IUsable usableObject = null;
        objectInHand?.TryGetComponent<IUsable>(out usableObject);

        if (!interactable.IsInteractionPossible(objectInHand != null) || (usableObject != null && usableObject.IsBeingUsed()))
            return;

        containerGameObject.SetActive(true);
        interactableObjectPrompt.text = interactable.GetInteractPrompt(objectInHand?.name);
    }

    private void HideUI()
    {
        containerGameObject.SetActive(false);
    }
}
