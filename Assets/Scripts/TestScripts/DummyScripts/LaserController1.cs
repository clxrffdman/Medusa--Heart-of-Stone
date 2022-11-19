using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController1 : MonoBehaviour
{
    private PlayerController playerController;

    public float baseLaserCapacity;
    public float currentLaserCapacity;
    public float laserRegenRate;
    public float laserDrainRate;
    public float drainTimer;
    public GameObject laserBarElement;
    public float initialYUIScale;

    private float currentDrainTimer;

    public Laser laser;
    public Laser extraLaser;
    public GameObject laserEndPoint;
    public CameraShake cameraHolder;

    void Start()
    {
        if(laserBarElement != null)
        {
            initialYUIScale = laserBarElement.transform.localScale.y;
        }
        
        playerController = FindObjectOfType<PlayerController>();
    }

    public void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (CheckIfLaserCanFire())
            {
                if (!laser.isActive)
                {
                    ActivateLaser();
                    CheckAndFireReflection();
                    GetComponentInParent<PlayerController>().isLaserControlled = true;
                    GetComponentInParent<PlayerController>().speed = GetComponentInParent<PlayerController>().laserSpeed;
                    
                }

                if (laser.isActive)
                {
                    CheckAndFireReflection();
                    cameraHolder.shakeTimer = 0.2f;
                }
            }
            else
            {
                DeactivateLaser();
                GetComponentInParent<PlayerController>().isLaserControlled = false;
                GetComponentInParent<PlayerController>().speed = GetComponentInParent<PlayerController>().baseSpeed;
            }
        }
        else
        {
            if (laser.isActive)
            {
                GetComponentInParent<PlayerController>().isLaserControlled = false;
                GetComponentInParent<PlayerController>().speed = GetComponentInParent<PlayerController>().baseSpeed;
                DeactivateLaser();
            }
        }

        UpdateLaserResources();
    }

    public void ActivateLaser()
    {
        laser.EnableLaser();
        
    }

    public void CheckAndFireReflection()
    {
        if (extraLaser != null)
        {
            if (extraLaser.firePoint.GetComponent<LaserReflector>().isOnReflect)
            {
                if (!extraLaser.isActive)
                {
                    extraLaser.EnableLaser();
                }
                
            }
            else
            {
                extraLaser.DisableLaser();
            }

        }
    }

    public void DeactivateLaser()
    {
        laser.DisableLaser();
        if (extraLaser != null){

            extraLaser.DisableLaser();
        }
        
    }

    public bool CheckIfLaserCanFire()
    {
        bool canFire = false;

        if(!GameManager.Instance.inventoryActive && !GameManager.Instance.dialogueActive && !GameManager.Instance.pauseMenuActive)
        {
            if (currentLaserCapacity > 0)
            {
                if (!playerController.laserEnabled)
                {
                    canFire = true;
                }
            }
        }

        
        return canFire;

    }

    public void UpdateLaserResources()
    {
        if(currentDrainTimer >= 0)
        {
            currentDrainTimer -= Time.deltaTime;
        }

        if (laser.isActive && currentLaserCapacity > 0)
        {
            currentLaserCapacity -= laserDrainRate;
            currentDrainTimer = drainTimer;
        }

        if (!laser.isActive && currentLaserCapacity < baseLaserCapacity && currentDrainTimer <= 0)
        {
            currentLaserCapacity += laserRegenRate;
        }

        if (laserBarElement != null)
        {
            laserBarElement.transform.localScale = new Vector3(laserBarElement.transform.localScale.x, initialYUIScale * ((0.0f + currentLaserCapacity) / (0.0f + baseLaserCapacity)), laserBarElement.transform.localScale.z);
        }
    }
}
