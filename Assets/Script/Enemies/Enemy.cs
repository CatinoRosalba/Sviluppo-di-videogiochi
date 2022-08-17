using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;                                                                    //Vita nemico
    public string weak;                                                                     //Debolezza nemico
    private GameObject drop;                                                                //Drop nemico

    private Object explosionRef;                                                            //Animazione morte

    private void Start()
    {
        explosionRef = Resources.Load("Particles/Explosion");

        if (gameObject.name.Contains("Fire"))          //Se il nome del nemico � Fire Enemy
        {
            health = 3;                                                                         //Setti vita
            weak = "water";                                                                     //Setti debolezza
            drop = Resources.Load<GameObject>("Gems/FireGem");                                       //Setti drop
        } 
        else if(gameObject.name.Contains("Water"))    //Se il nome del nemico � Water Enemy
        {
            health = 5;                                                                         //Setti vita
            weak = "fire";                                                                      //Setti debolezza
            drop = Resources.Load<GameObject>("Gems/WaterGem");                                      //Setti drop
        }
    }

    private void Update()
    {
        if (health <= 0)                                                                        //Se la vita � uguale o minore di 0
        {
            Destroy(gameObject);                                                                //Distruggi nemico
            Instantiate(drop, gameObject.transform.position, Quaternion.identity);              //Istanzia Drop

            GameObject explosion = (GameObject)Instantiate(explosionRef);                       //Esplosione
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            explosion.transform.rotation = gameObject.transform.rotation;
        }
    }
}
