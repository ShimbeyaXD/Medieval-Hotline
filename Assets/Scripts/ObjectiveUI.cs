using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectiveText;
    [SerializeField] TextMeshProUGUI getOutText;
    [SerializeField] Image objectiveImage;

    Artifact artifact;
    DialogueManager dialogueManager;

    void Start()
    {
        artifact = FindObjectOfType<Artifact>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        objectiveText.gameObject.SetActive(true);
        getOutText.gameObject.SetActive(false);

        objectiveText.text = "Current Objective:";
    }

    public void ObjectiveImage(Sprite sprite)
    {
        objectiveImage.sprite = sprite;
    }

    public void GetOutText()
    {
        getOutText.gameObject.SetActive(true);
        objectiveText.gameObject.SetActive(false);

    }
}
