using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace com.euzhene.Dialogs
{
    public class DialogInfo
    {
        public string titleText = "Default title";
        public string messageText = "Default message text";
        public string actionText = "Do smth";
        public UnityAction action;
        public bool enabled = false;

    }

    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private GameObject dialogCanvas;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Button actionButton;
        [SerializeField] private TMP_Text actionButtonText;
        [SerializeField] private Animator dialogAnimator;
        public static DialogUI Instance;
        private DialogInfo dialogInfo;

        void Awake()
        {
            Instance = this;
            dialogInfo = new DialogInfo();
        }

        public DialogUI SetTitle(string titleText)
        {
            dialogInfo.titleText = titleText;
            return Instance;
        }
        public DialogUI SetMessage(string messageText)
        {
            dialogInfo.messageText = messageText;
            return Instance;
        }
        public DialogUI SetActionText(string actionText)
        {
            dialogInfo.actionText = actionText;
            return Instance;
        }

        public DialogUI SetOnClick(UnityAction action)
        {
            dialogInfo.action = action;
            return Instance;
        }


        public void Show()
        {
            dialogInfo.enabled = true;
            dialogCanvas.SetActive(true);
            titleText.text = dialogInfo.titleText;
            messageText.text = dialogInfo.messageText;
            actionButtonText.text = dialogInfo.actionText;
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(dialogInfo.action);

            dialogAnimator.SetTrigger("Show");
        }

        public void Hide()
        {
            StopCoroutine("HideDialogAnimation");
            StartCoroutine(HideDialogAnimation());
        }

        private IEnumerator HideDialogAnimation()
        {
            dialogAnimator.SetTrigger("Hide");
            yield return new WaitForSeconds(1f);
            dialogCanvas.SetActive(false);
            dialogInfo = new DialogInfo();
        }
    }
}