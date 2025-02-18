using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject objectToDisable; // Référence à l'objet à désactiver
    private bool isInRange;
    private Text interactUI;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            interactUI.enabled = false;
            DialogueManager.instance.EndDialogue();

        }
    }

    void TriggerDialogue()
    {
        objectToDisable.SetActive(false); // Désactiver l'objet
        DialogueManager.instance.StartDialogue(dialogue, OnDialogueEnd);
    }

    void OnDialogueEnd()
    {
        objectToDisable.SetActive(true); // Réactiver l'objet
    }
}
