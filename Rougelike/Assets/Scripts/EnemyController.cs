using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    public Animator anim;

    public int health = 150;

    public GameObject[] deathSplatters;

    public bool shouldShoot;
    
    public GameObject bullet;
    public Transform firePoint; 
    public float fireRate; 
    private float fireCounter;
    public float shootRange;

    public SpriteRenderer theBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
       if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
       {
        moveDirection = PlayerController.instance.transform.position - transform.position;
       } 
       else 
       {
        moveDirection = Vector3.zero;
       }

       moveDirection.Normalize();

       theRB.velocity = moveDirection * moveSpeed;




       if(moveDirection != Vector3.zero)
       {
        anim.SetBool("isMoving", true);
       }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if(shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
        {
            fireCounter -= Time.deltaTime;

            if(fireCounter <=0)
            {
                fireCounter = fireRate;
                Instantiate(bullet, firePoint.transform.position, firePoint.rotation);

                AudioManager.instance.PlaySFX(13);
            }

        }
        } else{
            theRB.velocity = Vector2.zero;
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        if(health <=0)
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(1);

            int selectedSplatter = Random.Range(0, deathSplatters.Length);

            int rotation = Random.Range(0, 4);

            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90));

            //Instantiate(deathSplatters, transform.position, transform.rotation);
        }
    }
}
