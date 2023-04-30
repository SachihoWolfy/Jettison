using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool hurt = false;
    public bool angry;
    public GameObject Bullet;
    private Animator animator;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] Transform gunSpawn;
    [SerializeField] Transform gunSpawn2;
    [SerializeField] Transform gunSpawn3;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (angry)
        {
            animator.SetBool("Angry", true);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            if (!hurt)
            {
                _particleSystem.Play();
            }
            hurt = true;
            animator.SetBool("Hurt", true);
            StartCoroutine(Die());
        }

    }


    bool ShouldDieFromCollision(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;
        if (collision.contacts[0].normal.y < -0.5)
            return true;
        return false;
    }
    
    public void FireProjectile()
    {
        Quaternion rotationAmount = Quaternion.Euler(0, 0, 270);
        GameObject bulletInstance = Instantiate(Bullet, gunSpawn.position, gunSpawn.rotation * rotationAmount);
        GameObject bulletInstance2 = Instantiate(Bullet, gunSpawn2.position, gunSpawn2.rotation * rotationAmount);
        GameObject bulletInstance3 = Instantiate(Bullet, gunSpawn3.position, gunSpawn3.rotation * rotationAmount);
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(gunSpawn.up * 600);
        bulletInstance2.GetComponent<Rigidbody2D>().AddForce(gunSpawn2.up * 600);
        bulletInstance3.GetComponent<Rigidbody2D>().AddForce(gunSpawn3.up * 600);
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
