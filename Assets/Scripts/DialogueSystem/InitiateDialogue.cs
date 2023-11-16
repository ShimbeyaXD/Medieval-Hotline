using UnityEngine;

public class InitiateDialogue : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float interactionDistance = 0.5f;

    bool once = true;
    Vector3 direction;

    DialogueInteract dialogueInteract;

    private void Start()
    {
        dialogueInteract = GetComponent<DialogueInteract>();
    }

    void Update()
    {
        direction = transform.TransformDirection(Vector3.forward) * interactionDistance;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, interactionDistance))
        {
            Debug.Log("1");

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("3");

                if (once && Input.GetKeyDown(KeyCode.E))
                {
                    once = false;
                    InitiateChat();
                }
            }
        }
        else { once = true; }
    }

    void InitiateChat()
    {
        dialogueInteract.StartDialogue();
    }

}
