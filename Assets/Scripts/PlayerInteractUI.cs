using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.Collections;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private TextMeshProUGUI interactableObjectPrompt;

    //Dialogue Box Parameters
    [Header("Dialogue Box Parameters")]
    [SerializeField] private CanvasGroup dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueBoxText;
    [SerializeField] private TextMeshProUGUI ellipsis;

    [Header("Notepad Parameters")]
    [SerializeField] private GameObject notepadIcon;
    [SerializeField] bool showNotepadIcon = true;

    [Header("Hud Parameters")]
    [SerializeField] private GameObject paletteHud;
    [SerializeField] private GameObject paletteInputHelp;
    [SerializeField] private GameObject cursor;

    private bool showingDialogue = false;
    private bool continueDialogue = false;
    private Coroutine dialogueCoroutine;

    public bool ShowingDialogue { get => showingDialogue;}

    // Update is called once per frame
    void Update()
    {
        if (playerInteraction.GetInteractableObject() != null && !showingDialogue)
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

    public void ShowPaletteHud(bool show)
    {
        paletteHud.SetActive(show);
        paletteInputHelp.SetActive(show);
    }

    public void ShowCursor(bool show)
    {
        cursor.SetActive(show);
    }

    public void ShowDialogue(List<string> dialogueList, Action<IInteractable> OnDialogueFinished, IInteractable interactable)
    {
        if (dialogueList == null || dialogueList.Count == 0)
            return;

        if (dialogueCoroutine == null)
        {
            dialogueBox.gameObject.SetActive(true);
            dialogueCoroutine = StartCoroutine(DisplayDialogue(dialogueList, OnDialogueFinished, interactable));
            showingDialogue = true;
        }
    }

    public void ContinueDialogue()
    {
        continueDialogue = true;
    }

    private IEnumerator DisplayDialogue(List<string> dialogueList, Action<IInteractable> OnDialogueFinished, IInteractable interactable)
    {
        dialogueBox.alpha = 0f;

        for (int i = 0; i < dialogueList.Count; i++)
        {
            dialogueBoxText.text = dialogueList[i];

            while (dialogueBox.alpha < 1f)
            {
                dialogueBox.alpha += Time.deltaTime * 2;
                yield return null;
            }

            while (!continueDialogue)
            {
                ellipsis.text = ellipsis.text + ".";
                if (ellipsis.text.Length > 3)
                    ellipsis.text = string.Empty;

                yield return new WaitForSeconds(0.5f);
                yield return null;
            }
            ellipsis.text = string.Empty;
            continueDialogue = false;

            yield return new WaitForSeconds(0.2f);

            while (dialogueBox.alpha > 0f)
            {
                dialogueBox.alpha -= Time.deltaTime * 2;
                yield return null;
            }
        }

        dialogueCoroutine = null;
        showingDialogue = false;
        continueDialogue = false;
        dialogueBox.gameObject.SetActive(false);

        OnDialogueFinished?.Invoke(interactable);
    }

    public void ShowNotepadIcon(bool show)
    {
        if (!showNotepadIcon)
            return;

        notepadIcon.SetActive(show);
    }
}
