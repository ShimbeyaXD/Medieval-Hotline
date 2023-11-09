using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void StartAnim(string boolName)
    {
        myAnimator.SetBool(boolName, true);
    }

    public void StopAnim(string boolName)
    {
        myAnimator.SetBool(boolName, false);
    }
}
