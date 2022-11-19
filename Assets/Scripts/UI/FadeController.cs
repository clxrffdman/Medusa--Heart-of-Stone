using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{

    private Image fadeObject;
    private GameObject testObject;
    public bool isFadingOut { get; private set; }
    public bool isFadingIn { get; private set; }
    [HideInInspector]
    public float fadeTime;

    void Awake()
    {
        fadeObject = GameObject.Find("FadeObject").GetComponent<Image>();
    }

    void Update()
    {

        if (isFadingOut)
        {
            Color objectColor = fadeObject.color;
            float fadeAmount = objectColor.a + (Time.deltaTime / fadeTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeObject.color = objectColor;

            if (objectColor.a >= 1f)
            {
                isFadingOut = false;
            }
        }

        if (isFadingIn)
        {
            Color objectColor = fadeObject.color;
            float fadeAmount = objectColor.a - (Time.deltaTime / fadeTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeObject.color = objectColor;

            if (objectColor.a <= 0f)
            {
                isFadingIn = false;
            }
        }
    }

    public void FadeOut(float fadeTime)
    {
        this.fadeTime = fadeTime; 
        isFadingOut = true;
        isFadingIn = false;
    }

    public void FadeIn(float fadeTime)
    {
        this.fadeTime = fadeTime;
        isFadingOut = false;
        isFadingIn = true;
    }

    public void SetScreenToBlack()
    {
        fadeObject.color = new Color(0, 0, 0);
    }
}
