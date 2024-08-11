using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    public Action OnShoot;
    public Bullet BulletPrefab;
    public Bullet Bullet;
    public Transform BulletSpawnPoint;
    public GameObject ShootTarget;
    public List<GameObject> enemies;
    public float distanceToShoot;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shoot2());
    }

    private void OnEnable()
    {
        OnShoot += ShootProjectile;
    }

    private void ShootProjectile()
    {
        Bullet = Instantiate(BulletPrefab);
        Bullet.Init(BulletSpawnPoint.position, ShootTarget.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    Shoot();
        //}
    }



    IEnumerator shoot2()
    {
        int nearestEnemyId = -1;
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            nearestEnemyId = FindNearestObject(distanceToShoot,enemies);
            if (nearestEnemyId >= 0)
            {
                ShootTarget.transform.position = enemies[nearestEnemyId].transform.position;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        OnShoot?.Invoke();
    }

    public int FindNearestObject(float range,List<GameObject> objList)
    {
        int nearestID = -1;
        float nearestDistance = Mathf.Infinity;
        float distancetoEnemy = 0;
        for (int i = 0; i < objList.Count; i++)
        {
            distancetoEnemy = Vector3.Distance(transform.position, objList[i].transform.position);
            if (distancetoEnemy < nearestDistance)
            {
                nearestID = i;
                nearestDistance = distancetoEnemy;
            }
        }
        if (nearestDistance < distanceToShoot)
            return nearestID;
        else return -1;
    }
}
