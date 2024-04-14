using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxHandler : MonoBehaviour
{
    Vector2 mousePz;

    [Header("KnightImage")]
    [SerializeField] GameObject knightImage;
    [SerializeField] float parallaxModifier;
    Vector2 knightStartPos;

    [Header("Cultes1")]
    [SerializeField] GameObject cultes1Image;
    [SerializeField] float cultesParallaxModifier;
    Vector2 cultes1StartPos;

    [Header("Cultes2")]
    [SerializeField] GameObject cultes2Image;
    [SerializeField] float cultes2ParallaxModifier;
    Vector2 cultes2StartPos;

    [Header("Pile1")]
    [SerializeField] GameObject pile1Image;
    [SerializeField] float xPile1ParallaxModifier;
    [SerializeField] float yPile1ParallaxModifier;
    Vector2 pile1StartPos;

    [Header("Pile2")]
    [SerializeField] GameObject pile2Image;
    [SerializeField] float xPile2ParallaxModifier;
    [SerializeField] float yPile2ParallaxModifier;
    Vector2 pile2StartPos;

    // Start is called before the first frame update
    void Start()
    {
        knightStartPos = knightImage.transform.position;
        cultes1StartPos = cultes1Image.transform.position;
        cultes2StartPos = cultes2Image.transform.position;
        pile1StartPos = pile1Image.transform.position;
        pile2StartPos = pile2Image.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mousePz = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        KnightImageParralax();
        Cultes1Parallax();
        Cultes2Parallax();
        PileParallax();
        Pile2Parallax();
    }

    void KnightImageParralax() 
    {
        float xPos = Mathf.Lerp(knightImage.transform.position.x, knightStartPos.x + (mousePz.x * parallaxModifier), 2f * Time.deltaTime);
        float yPos = Mathf.Lerp(knightImage.transform.position.y, knightStartPos.y + (mousePz.y * parallaxModifier), 2f * Time.deltaTime);

        knightImage.transform.position = new Vector3(xPos, yPos, 0f);
    }

    void Cultes1Parallax() 
    {

        float xPos = Mathf.Lerp(cultes1Image.transform.position.x, cultes1StartPos.x + (mousePz.x * cultesParallaxModifier), 2f * Time.deltaTime);
        float yPos = Mathf.Lerp(cultes1Image.transform.position.y, cultes1StartPos.y + (mousePz.y * cultesParallaxModifier), 2f * Time.deltaTime);

        cultes1Image.transform.position = new Vector3(xPos, yPos, 0f);
    }

    void Cultes2Parallax()
    {

        float xPos = Mathf.Lerp(cultes2Image.transform.position.x, cultes2StartPos.x + (mousePz.x * cultes2ParallaxModifier), 2f * Time.deltaTime);
        float yPos = Mathf.Lerp(cultes2Image.transform.position.y, cultes2StartPos.y + (mousePz.y * cultes2ParallaxModifier), 2f * Time.deltaTime);

        cultes2Image.transform.position = new Vector3(xPos, yPos, 0f);
    }

    void PileParallax()
    {

        float xPos = Mathf.Lerp(pile1Image.transform.position.x, pile1StartPos.x + (mousePz.x * xPile1ParallaxModifier), 2f * Time.deltaTime);
        float yPos = Mathf.Lerp(pile1Image.transform.position.y, pile1StartPos.y + (mousePz.y * yPile1ParallaxModifier), 2f * Time.deltaTime);

        pile1Image.transform.position = new Vector3(xPos, yPos, 0f);
    }

    void Pile2Parallax()
    {

        float xPos = Mathf.Lerp(pile2Image.transform.position.x, pile2StartPos.x + (mousePz.x * xPile2ParallaxModifier), 2f * Time.deltaTime);
        float yPos = Mathf.Lerp(pile2Image.transform.position.y, pile2StartPos.y + (mousePz.y * yPile2ParallaxModifier), 2f * Time.deltaTime);

        pile2Image.transform.position = new Vector3(xPos, yPos, 0f);
    }

}
