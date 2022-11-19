using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Vector2 direction;
    public List<ParticleSystem> particles = new List<ParticleSystem>();
    private AudioSource audioSource;

    public LayerMask collidableLayers;
    private int layerMask = 8;
    private int playerLayerMask = 11;
    

    public bool isActive { get; private set; }
    [Header("Objects")]
    public LineRenderer lineRenderer;
    public GameObject firePoint;
    public GameObject laserEndPoint;
    public GameObject hitPoint;
    public bool isReflection;
    public GameObject ogPoint;
    public GameObject closestHit;
    public float closestHitDistance;
    public Vector3 closestHitPointPosition;

    public int damageAmount;
    public float distance;
    public bool canAttack;
    public float verticalCheckDistance;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damageAmount);
            }
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lineRenderer.useWorldSpace = true;
        FillList();
        DisableLaser();
    }

    void Update()
    {

        UpdateLaser();

    }

    public void EnableLaser()
    {
        isActive = true;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }
        lineRenderer.enabled = true;
        
    }

    public void DisableLaser()
    {
        isActive = false;
        lineRenderer.enabled = false;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
           
    }

    public void UpdateLaser()
    {
        if (!isReflection)
        {
            PlaceLaserEndPointAtMouseCursor();
        }
        else
        {

            PlaceLaserEndPointAtReflectedPoint();
            if (firePoint.GetComponent<LaserReflector>().isOnReflect)
            {
                
            }
            else
            {
                return;
            }
            
        }
        
        lineRenderer.SetPosition(0, (Vector2)firePoint.transform.position);
        lineRenderer.SetPosition(1, (Vector2)laserEndPoint.transform.position);

        direction = (Vector2)laserEndPoint.transform.position - (Vector2)firePoint.transform.position;
        distance = Vector3.Distance(firePoint.transform.position, laserEndPoint.transform.position);

        Debug.DrawRay((Vector2)firePoint.transform.position, direction.normalized * distance, Color.red);
        RaycastHit2D[] hitArray;
        hitArray = Physics2D.RaycastAll((Vector2)firePoint.transform.position, direction.normalized, distance, collidableLayers);
        Debug.DrawRay((Vector2)firePoint.transform.position, direction.normalized * distance, Color.blue);
        closestHit = null;
        string s = "";
        foreach(RaycastHit2D r in hitArray)
        {
            s += r.transform.gameObject.name + " ";
        }
        if (isActive)
        {
            //print(s);
        }
        
        foreach(RaycastHit2D r in hitArray)
        {
            

            if(r.collider.gameObject.GetComponentInParent<EnemyController>() != null && r.collider.gameObject.GetComponentInParent<EnemyController>().isDead && (r.collider.gameObject.transform.position.y < (transform.position.y + verticalCheckDistance)) && (r.collider.gameObject.transform.position.y + verticalCheckDistance > transform.position.y))
            {

            }
            else
            {
                if(closestHit == null)
                {
                    closestHit = r.transform.gameObject;
                    closestHitDistance = r.distance;
                    closestHitPointPosition = r.point;
                }
                else
                {
                    if (r.distance < closestHitDistance)
                    {
                        closestHit = r.transform.gameObject;
                        closestHitDistance = r.distance;
                        closestHitPointPosition = r.point;
                    }
                }
            }

            
        }

        if(closestHit != null)
        {
            lineRenderer.SetPosition(1, closestHitPointPosition);
            distance = Vector3.Distance(firePoint.transform.position, closestHitPointPosition);
            if (isActive && closestHit.tag == "EnemyHurtbox")
            {  
                closestHit.GetComponentInParent<EnemyHealth>().TakeDamage(1);
            }
        }

        hitPoint.transform.position = lineRenderer.GetPosition(1);
    }

    public void PlaceLaserEndPointAtMouseCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3(mousePos.x, mousePos.y, 0f);
        laserEndPoint.transform.position = targetPos;
    }

    public void PlaceLaserEndPointAtReflectedPoint()
    {
        float newAngle = Mathf.Rad2Deg * Mathf.Atan((ogPoint.transform.position.y - firePoint.transform.position.y) / (ogPoint.transform.position.x - firePoint.transform.position.x));
        Vector3 targetPos = new Vector3((firePoint.transform.position.x - ogPoint.transform.position.x)*2 + ogPoint.transform.position.x, ogPoint.transform.position.y, 0);
        laserEndPoint.transform.position = targetPos;
    }

    public void FillList()
    {
        if (!isReflection)
        {
            for (int i = 0; i < firePoint.transform.childCount; i++)
            {
                ParticleSystem ps = firePoint.transform.GetChild(i).GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    particles.Add(ps);
                }
            }
        }
        

        for (int i = 0; i < hitPoint.transform.childCount; i++)
        {
            ParticleSystem ps = hitPoint.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
    }

    public void EnableAttack()
    {
        canAttack = true;
    }

    public void DisableAttack()
    {
        canAttack = false;
    }

    
}