using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHot : MonoBehaviour
{

    public Camera cam;
    public GameObject bulletPrefab;
    GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            bullet = Instantiate(bulletPrefab, cam.transform.position, cam.transform.rotation);
    }
}
