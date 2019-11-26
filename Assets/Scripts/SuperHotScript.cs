using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SuperHotScript : MonoBehaviour
{

    public static SuperHotScript instance;

    public float charge;
    public bool canShoot = true;
    public bool action;
    public GameObject bullet;
    public Transform bulletSpawner;

    [Header("Weapon")]
    public WeaponScript weapon;
    public Transform weaponHolder;
    public LayerMask weaponLayer;


    [Space]
    [Header("UI")]
    public Image indicator;

    [Space]
    [Header("Prefabs")]
    public GameObject hitParticlePrefab;
    public GameObject bulletPrefab;


    private void Awake()
    {
        instance = this;
        if (weaponHolder.GetComponentInChildren<WeaponScript>() != null)
            weapon = weaponHolder.GetComponentInChildren<WeaponScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        if (canShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StopCoroutine(ActionE(.03f));
                StartCoroutine(ActionE(.03f));
                if (weapon != null)
                    weapon.Shoot(SpawnPos(), Camera.main.transform.rotation, false);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            StopCoroutine(ActionE(.4f));
            StartCoroutine(ActionE(.4f));

            if(weapon != null)
            {
                weapon.Throw();
                weapon = null;
            }
        }

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,10, weaponLayer))
        {
            if (Input.GetMouseButtonDown(0) && weapon == null)
            {
                hit.transform.GetComponent<WeaponScript>().Pickup();
            }
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float time = (x != 0 || y != 0) ? 1f : .03f;
        float lerpTime = (x != 0 || y != 0) ? .05f : .5f;

        time = action ? 1 : time;
        lerpTime = action ? .1f : lerpTime;

        Time.timeScale = Mathf.Lerp(Time.timeScale, time, lerpTime);
    }

    IEnumerator ActionE(float time)
    {
        action = true;
        yield return new WaitForSecondsRealtime(.06f);
        action = false;
    }

    public void ReloadUI(float time)
    {
        indicator.transform.DORotate(new Vector3(0, 0, 90), time, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(() => indicator.transform.DOPunchScale(Vector3.one / 3, .2f, 10, 1).SetUpdate(true));
    }


    Vector3 SpawnPos()
    {
        return Camera.main.transform.position + (Camera.main.transform.forward * .5f) + (Camera.main.transform.up * -.02f);
    }


}
