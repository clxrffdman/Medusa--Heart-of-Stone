using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightTest : MonoBehaviour
{

    public bool spotted;
    public GameObject player;
    public List<Vector2> positionsToCycle;
    public float cycleTime;
    public float randomnessFactor;
    public float lingerTime;
    public float lingerTimeRandomnessFactor;
    public int sideState;
    public SpriteRenderer towerSpriteRenderer;
    public Sprite[] towerStates;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        sideState = 0;
        StartCoroutine(PositionCycling());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public IEnumerator PositionCycling()
    {
        while (true)
        {
            foreach(Vector2 vec in positionsToCycle)
            {

                LeanTween.move(gameObject, vec, cycleTime + Random.Range(-randomnessFactor, randomnessFactor)).setEase(LeanTweenType.easeInOutQuad);
                if(sideState == 0)
                {
                    sideState = 1;
                    if(towerSpriteRenderer != null)
                    {
                        towerSpriteRenderer.sprite = towerStates[1]; 
                    }
                }
                else
                {
                    sideState = 0;
                    towerSpriteRenderer.sprite = towerStates[0];
                }

                yield return new WaitForSeconds(cycleTime + Random.Range(-randomnessFactor, randomnessFactor));
                
                yield return new WaitForSeconds(lingerTime + Random.Range(-lingerTimeRandomnessFactor, lingerTimeRandomnessFactor));
            }
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHurtbox")
        {
            spotted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHurtbox")
        {
            spotted = false;
        }
    }
}
