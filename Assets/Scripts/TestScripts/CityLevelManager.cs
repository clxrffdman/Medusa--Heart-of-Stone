using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLevelManager : MonoBehaviour
{
    public CityLevelChecker[] levelCheckers;
    public float transitionTime;
    
    void FixedUpdate()
    {
        foreach(CityLevelChecker c in levelCheckers)
        {
            if (c.isLevel)
            {
                if (c.isGreyed && !c.isTransition)
                {
                    StartCoroutine(FadeOut(c));
                }
                

            }
            else
            {
                if (!c.isGreyed && !c.isTransition)
                {
                    StartCoroutine(FadeIn(c));
                }

                
            }
        }
    }

    public IEnumerator FadeOut(CityLevelChecker c)
    {
        c.isTransition = true;
        LeanTween.value(gameObject, 0.4f, 1f, transitionTime).setOnUpdate((float val) => {

            foreach(GameObject g in c.tiedBackdrop)
            {
                if(g.transform.childCount > 0)
                {
                    for(int i = 0; i < g.transform.childCount; i++)
                    {
                        if (g.transform.GetChild(i).GetComponent<SpriteRenderer>())
                        {
                            g.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(val, val, val, 1);
                        }
                    }
                }
                else
                {
                    if (g.transform.GetComponent<SpriteRenderer>())
                    {
                        g.transform.GetComponent<SpriteRenderer>().color = new Color(val, val, val, 1);
                    }
                }
            }

            
        });
        yield return new WaitForSeconds(transitionTime);
        c.isGreyed = false;
        c.isTransition = false;

    }

    public IEnumerator FadeIn(CityLevelChecker c)
    {
        c.isTransition = true;
        LeanTween.value(gameObject, 1f, 0.4f, transitionTime).setOnUpdate((float val) => {

            foreach (GameObject g in c.tiedBackdrop)
            {
                if (g.transform.childCount > 0)
                {
                    for (int i = 0; i < g.transform.childCount; i++)
                    {
                        if (g.transform.GetChild(i).GetComponent<SpriteRenderer>())
                        {
                            g.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(val, val, val, 1);
                        }
                    }
                }
                else
                {
                    if (g.transform.GetComponent<SpriteRenderer>())
                    {
                        g.transform.GetComponent<SpriteRenderer>().color = new Color(val, val, val, 1);
                    }
                }
            }


        });
        yield return new WaitForSeconds(transitionTime);
        c.isGreyed = true;
        c.isTransition = false;

    }
}
