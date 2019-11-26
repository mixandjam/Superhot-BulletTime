using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BodyPartScript bp = collision.gameObject.GetComponent<BodyPartScript>();

            //if (!bp.enemy.dead)
                Instantiate(SuperHotScript.instance.hitParticlePrefab, transform.position, transform.rotation);

            bp.HidePartAndReplace();
            bp.enemy.Ragdoll();
        }
        Destroy(gameObject);
    }
}
