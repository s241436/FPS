using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{



    public float projectileLife = 3.0f;
    public int damageAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, projectileLife);
        
    }

  
    private void OnCollisionEnter(Collision objectWeHit)
    {
        Destroy(gameObject);


        if (objectWeHit.gameObject.CompareTag("Target1"))
        {
            TargetHealth targetHit = null;

            targetHit = objectWeHit.gameObject.GetComponent<TargetHealth>();

            // Damage the target
            targetHit.Damage(damageAmount);

            // call bullet impact effect
            CreateBulletImpacEffect(objectWeHit);

            objectWeHit.gameObject.GetComponent<AudioSource>().Play();

            // destroy bullet
            Destroy(gameObject);

        }

        

        if (objectWeHit.gameObject.CompareTag("wall"))
        {
            Debug.Log("hit a wall");

            CreateBulletImpacEffect(objectWeHit);

            Destroy(gameObject);
        }



        

        
    }

    void CreateBulletImpacEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(GlobalReferences.Instance.bulletImpactEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }

}
