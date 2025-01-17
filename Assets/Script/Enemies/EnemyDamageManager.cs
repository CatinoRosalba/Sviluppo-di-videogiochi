using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    private GameObject sfx;
    private AudioSource damageSound;

    private Enemy enemy;                                                                            //Riferimento allo script Enemy
    private bool canDamage;                                                                         //Permette di creare gli invisibility frame
    private Material matDefault;                                                                    //materiale di default
    private Material matWhite;                                                                      //material di colore bianco
    private float flashTime = .10f;                                                                 //tempo del flash
    public bool canActivate;

    private void Start()
    {
        sfx = GameObject.Find("SFX");
        damageSound = sfx.transform.Find("SFX - Enemy Takes Damage").GetComponent<AudioSource>();

        enemy = gameObject.GetComponent<Enemy>();
        canDamage = true;
        matWhite = Resources.Load("Particles/FlashWhite", typeof(Material)) as Material;
        
        //accesso allo sprite figlio dell'oggetto padre Enemy
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            matDefault = r.material;
        }
    }

    public void TakeDamage(float damage, string type)
    {
        if (canDamage == true)                                                  //Se pu� subire danno
        {
            if (type == enemy.weak)                                             //Se il nemico � debole
            {
                enemy.health -= damage * 2f;                                    //Fai due volte il danno
            }
            else
            {
                enemy.health -= damage;                                         //Calcola il danno
            }
            damageSound.Play();
            ActivateOnDamage();
            StartCoroutine(EFlash());                                           //Flash del danno
        }
    }

    //Timer del flash
    IEnumerator EFlash()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material = matWhite;
            yield return new WaitForSeconds(flashTime);
            r.material = matDefault;
        }

    }

    private void ActivateOnDamage()
    {
        if (canActivate)
        {
            if (gameObject.TryGetComponent<FireEnemyAttack>(out FireEnemyAttack Fattack) && gameObject.TryGetComponent<FireEnemyMovement>(out FireEnemyMovement Fmov))
            {
                if (!Fattack.enabled || !Fmov.enabled)
                {
                    Fattack.enabled = true;
                    Fmov.enabled = true;
                }
            }

            if (gameObject.TryGetComponent<WaterEnemyAttack>(out WaterEnemyAttack Wattack) && gameObject.TryGetComponent<WaterEnemyMovement>(out WaterEnemyMovement Wmov))
            {
                if (!Wattack.enabled || !Wmov.enabled)
                {
                    Wattack.enabled = true;
                    Wmov.enabled = true;
                }
            }
        }
    }
}
