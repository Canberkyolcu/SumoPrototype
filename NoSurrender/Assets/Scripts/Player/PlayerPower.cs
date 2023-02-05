using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    [SerializeField] private EnemyAl enemyAl;

    

    public float pushPower;


    private Camera cam;
    public Rigidbody rb;

    void Start()
    {

        cam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        UIManager.Instance.countPlayer += 1;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Power")
        {
            Vector3 scale = new Vector3((float)0.02, (float)0.02, (float)0.02);
            this.gameObject.transform.localScale += scale;
            enemyAl.forcePower += 100f;
            Vector3 camY = cam.transform.position;
            camY.y += (float)0.01f;
            cam.transform.position = camY;
            other.gameObject.SetActive(false);

        }

        if (other.gameObject.tag == "EnemyBack")
        {
            if (enemyAl.forcePower >= pushPower )
            {
                enemyAl.enemyRb.AddForce(transform.forward * Mathf.Abs(pushPower - enemyAl.forcePower) * 4);
            }
            else
            {
                enemyAl.enemyRb.AddForce(transform.forward * 200);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (enemyAl.forcePower >= pushPower)
            {
                enemyAl.enemyRb.AddForce(transform.forward * Mathf.Abs(pushPower - enemyAl.forcePower) * 2);
            }
            else
            {
                enemyAl.enemyRb.AddForce(transform.forward * 200);
            }
        }

      

    }

    private IEnumerator OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            yield return new WaitForSeconds(1f);
           // Destroy(gameObject);
            UIManager.Instance.countPlayer -= 1;

        }
    }


}
