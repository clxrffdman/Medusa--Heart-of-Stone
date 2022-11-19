using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private PlayerController playerController;
    private AudioSource audioSource;

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

    private void Awake()
    {
        
    }
    void Start()
    {
        currentLaserCapacity = baseLaserCapacity;

        if (laserBarElement == null)
        {
            laserBarElement = GameObject.Find("SnakeBarUIElement").transform.GetChild(0).gameObject;
            
        }
        initialYUIScale = laserBarElement.transform.localScale.y;

        playerController = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        
    }

    public void Update()
    {

        if (!playerController.laserEnabled)
        {
            UpdateLaserResources();
            if (laser.isActive)
            {
                playerController.isLaserControlled = false;
                playerController.speed = playerController.attackSpeed;
                DeactivateLaser();
            }
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (CheckIfLaserCanFire())
            {
                if (!laser.isActive)
                {
                    ActivateLaser();
                    //CheckAndFireReflection();
                    playerController.isLaserControlled = true;
                    playerController.speed = playerController.laserSpeed;
                    
                }

                if (laser.isActive)
                {
                    //CheckAndFireReflection();
                    cameraHolder.shakeTimer = 0.2f;
                }
            }
            else
            {
                DeactivateLaser();
                playerController.isLaserControlled = false;
                playerController.speed = playerController.attackSpeed;
            }
        }
        else
        {
            if (laser.isActive)
            {
                playerController.isLaserControlled = false;
                playerController.speed = playerController.attackSpeed;
                DeactivateLaser();
            }
        }

        UpdateLaserResources();
    }

    public void ActivateLaser()
    {
        laser.EnableLaser();
        PlayLaserSound();
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
        StopLaserSound();
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
                if (playerController.laserEnabled)
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

    public void PlayLaserSound()
    {
        audioSource.Play();
    }

    public void StopLaserSound()
    {
        audioSource.Stop();
    }
}
