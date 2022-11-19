using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    private SpriteRenderer boxColliderSpriteRenderer;
    public bool isFadingOut = true;
    public Text instructionText;
    public float fadeTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!instructionText.gameObject.activeInHierarchy)
            {
                instructionText.gameObject.SetActive(true);
            }

            if (isFadingOut)
            {
                isFadingOut = false;
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!isFadingOut)
            {
                isFadingOut = true;
            }
        }    
    }

    void Start()
    {
        boxColliderSpriteRenderer = GetComponent<SpriteRenderer>();
        boxColliderSpriteRenderer.enabled = false;
        SetInitialAlphaToZero();
        instructionText.gameObject.SetActive(false);
    }

    void Update()
    {
        FadeInAndOut();
    }

    private void FadeInAndOut()
    {
        if (instructionText.color.a >= 1f && !isFadingOut)
        {
            return;
        }

        if (instructionText.color.a <= 0f && isFadingOut)
        {
            return;
        }

        if (!isFadingOut)
        {
            Color instructionColor = instructionText.color;
            float fadeAmount = instructionColor.a + (Time.deltaTime / fadeTime);
            instructionColor = new Color(instructionColor.r, instructionColor.g, instructionColor.b, fadeAmount);
            instructionText.color = instructionColor;
        }

        if (isFadingOut)
        {
            Color instructionColor = instructionText.color;
            float fadeAmount = instructionColor.a - (Time.deltaTime / fadeTime);
            instructionColor = new Color(instructionColor.r, instructionColor.g, instructionColor.b, fadeAmount);
            instructionText.color = instructionColor;
        }
    }

    private void SetInitialAlphaToZero()
    {
        Color instructionColor = instructionText.color;
        instructionColor = new Color(instructionColor.r, instructionColor.g, instructionColor.b, 0f);
        instructionText.color = instructionColor;
    }
}
