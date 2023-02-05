using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float powerRange;
    [SerializeField] private float turnSpeed;

    [SerializeField] public Rigidbody enemyRb;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] power;
    [SerializeField] private PlayerPower playerPower;
    

    public float forcePower;

    private float distancePlayer;
    private float distancePower;
    

    private Vector3 targetPlayer;

    public bool isGameStarted;

    enum state
    {
        Idle,
        Power,
        Attack
    }

    [SerializeField] private state currentState = state.Idle;

    void Start()
    {

        enemyRb = GetComponent<Rigidbody>();

        UIManager.Instance.countPlayer += 1;
    }


    void Update()
    {
        StateCheck();
        StateExecute();

    }

    private void StateCheck()
    {
        if (playerPower.pushPower <= forcePower)
        {
            currentState = state.Power;
        }
        else if (forcePower < playerPower.pushPower)
        {
            currentState = state.Attack;
        }
        else
        {
            currentState = state.Idle;
        }
    }

    private void StateExecute()
    {
        switch (currentState)
        {
            case state.Idle:
                break;
            case state.Power:
                PowerCollect();
                break;
            case state.Attack:
                EnemyAttack();
                break;
        }
    }


    private void EnemyAttack() 
    {
        if (isGameStarted)
        {
            distancePlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distancePlayer <= attackRange)
            {
                
                targetPlayer = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                Vector3 lookPos = new Vector3(targetPlayer.x, transform.position.y, targetPlayer.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos - transform.position),
                    turnSpeed * Time.deltaTime);

                transform.Translate(speed * Time.deltaTime * Vector3.forward);

            }
        }

    }

    private void PowerCollect()
    {

        if (isGameStarted)
        {
            power = GameObject.FindGameObjectsWithTag("Power");



            for (int i = 0; i < 1; i++) 
            {
                // For döngüsünü birden çok "Power" olduðu için kullanýp daha sonrasýnda 0'a eþitleme sebebim ise ayný anda "Power" adlý GameObjesini bulunca Buga giriyor bunu önlemek için böyle bir sistem kullandým.
                if (power[i] = power[0]) 
                {
                    distancePower = Vector3.Distance(transform.position, power[0].transform.position);


                    if (distancePower <= powerRange)
                    {
                        targetPlayer = new Vector3(power[0].transform.position.x, transform.position.y, power[0].transform.position.z);
                        Vector3 lookPos = new Vector3(targetPlayer.x, transform.position.y, targetPlayer.z);
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos - transform.position),
                            turnSpeed * Time.deltaTime);


                        transform.Translate(speed * Time.deltaTime * Vector3.forward);


                    }

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Power")
        {
            Vector3 scale = new Vector3((float)0.02, (float)0.02, (float)0.02);
            this.gameObject.transform.localScale += scale;
            playerPower.pushPower += 100;
            other.gameObject.SetActive(false);
            power[power.Length] = power[power.Length - 1];

        }

        if (other.gameObject.tag == "PlayerBack") 
        {
            if (playerPower.pushPower >= forcePower)
            {
                playerPower.rb.AddForce(transform.forward * Mathf.Abs(forcePower - playerPower.pushPower) * 4);
            }
            else
            {
                playerPower.rb.AddForce(transform.forward * 200);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerPower.pushPower >= forcePower)
            {
                playerPower.rb.AddForce(transform.forward * Mathf.Abs(forcePower - playerPower.pushPower) * 2);
            }
            else
            {
                playerPower.rb.AddForce(transform.forward * 200);
            }
        }

    }

    private IEnumerator OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
            UIManager.Instance.countPlayer -= 1;
            UIManager.Instance.isAlive = false;
        }
    }
}
