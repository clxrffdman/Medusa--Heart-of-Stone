using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateActionEnemy : ActivateAction
{
    public EnemyController[] enemiesToTrack;
    public bool isActivated;
    public string taskNeededForAlternateActionLoad;
    public GameObject alternateAction;

    private bool allEnemiesDead;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isActivated)
        {
            allEnemiesDead = true;

            foreach(EnemyController ec in enemiesToTrack) {

                if (!ec.isDead)
                {
                    allEnemiesDead = false;
                }

            }
        }

        if(allEnemiesDead == true)
        {
            isActivated = true;
            ActivateNextScene();
            this.gameObject.SetActive(false);
        }
    }

    protected override void ActivateNextScene()
    {
        actionController.DeactivateAllActions();

        if (TaskManager.Instance.CheckTaskComplete(taskNeededForAlternateActionLoad))
        {
            alternateAction.SetActive(true);
        }
        else
        {
            actionToActivate.gameObject.SetActive(true);
        }

        if (boxCollider) { boxCollider.enabled = false; }
    }
}
