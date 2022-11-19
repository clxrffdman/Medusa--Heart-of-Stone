using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Health : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    protected AudioSource myAudioSource;
    private PlayerController playerController;
    private ActionController actionController;

    public GameObject mainCamera;
    public bool isInvunerable;
    public Vignette v;
    public GameObject actionToReloadScene;

    [Header("Settings")]
    public int baseHealth;
    public int currentHealth;
    public Color damageColor;
    public float invincibilityTime;
    public float hurtDuration;
    public float filckerInterval;
    public float freezeDelay;
    public float freezeFrameTime;

    [Header("Objects")]
    public AudioClip[] audioClips;
    public GameObject healthParent;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        actionController = FindObjectOfType<ActionController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
        currentHealth = baseHealth;
        if(mainCamera == null)
        {
            if(GameObject.Find("CameraHolder") != null)
            {
                mainCamera = GameObject.Find("CameraHolder").transform.GetChild(0).gameObject;
            }
        }

        if (mainCamera != null)
        {
            mainCamera.GetComponent<Volume>().profile.TryGet<Vignette>(out v);
        }

        if(healthParent == null)
        {
            healthParent = GameObject.Find("HealthParent");
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateHealthBasedProcessing(float input)
    {
        v.intensity.value = input;
    }

    public void DamagePlayer(int damageAmount)
    {
        if (isInvunerable)
        {
            return;
        }

        if(!isInvunerable)
        {
            currentHealth -= damageAmount;
            if(currentHealth == 1)
            {
                LeanTween.value(gameObject, v.intensity.value, 0.45f, 0.2f).setOnUpdate((float val) => {
                    UpdateHealthBasedProcessing(val);
                });
                
            }
            else if (currentHealth == 2)
            {
                LeanTween.value(gameObject, v.intensity.value, 0.35f, 0.2f).setOnUpdate((float val) => {
                    UpdateHealthBasedProcessing(val);
                });
            }
            else if (currentHealth == 3)
            {
                LeanTween.value(gameObject, v.intensity.value, 0.2f, 0.2f).setOnUpdate((float val) => {
                    UpdateHealthBasedProcessing(val);
                });
            }

            isInvunerable = true;

            if(healthParent != null)
            {
                if(healthParent.transform.childCount > 0)
                {
                    healthParent.transform.GetChild(healthParent.transform.childCount - 1).gameObject.GetComponent<HeartAnimScript>().BreakHeart();

                    Destroy(healthParent.transform.GetChild(healthParent.transform.childCount - 1).gameObject, invincibilityTime);
                }
            }

            if (currentHealth <= 0)
            {
                KillPlayer();
                ReloadScene();
                return;
            }

            playerController.ChangeStateToHurt();
            Invoke("MakePlayerVulnerable", invincibilityTime);
            StartCoroutine(StartHurt());
            StartCoroutine("FlickerSprite");
        }
    }

    public IEnumerator FreezeFrame()
    {
        yield return new WaitForSeconds(freezeDelay);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(freezeFrameTime);
        Time.timeScale = 1;
    }

    public void LoadAudioClipIntoAudioSource(int audioClipNumber)
    {
        myAudioSource.clip = audioClips[audioClipNumber];
    }

    public void MakePlayerVulnerable()
    {
        isInvunerable = false;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public IEnumerator FlickerSprite()
    {
        spriteRenderer.color = damageColor;

        yield return new WaitForSeconds(filckerInterval);

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        yield return new WaitForSeconds(filckerInterval);

        if (isInvunerable)
        {
            StartCoroutine("FlickerSprite");
        }
    }

    public IEnumerator StartHurt()
    {
        yield return new WaitForSeconds(hurtDuration);
        playerController.ChangeStateToIdle();
    }

    public void KillPlayer()
    {
        playerController.ChangeStateToDead();
    }

    private void ReloadScene()
    {
        actionController.DeactivateAllActions();
        actionToReloadScene.SetActive(true);
    }
}
