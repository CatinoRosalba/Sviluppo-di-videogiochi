using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    //Scripts
    public UIManager slot;                                                      //Script dell'interfaccia degli slot
    private PlayerAim aim;                                                      //Script della mira

    //Sparo e ammo
    private GameObject bulletSpawnPoint;                                        //Spawn dei proiettili
    public GameObject equippedGem1;
    public GameObject primaryFire;                                              //Sparo col tasto sinistro del mouse
    public int primaryAmmo;                                                     //Munizioni per lo sparo primario
    public GameObject equippedGem2;
    public GameObject secondaryFire;                                            //Sparo col tasto destro del mouse
    public int secondaryAmmo;                                                   //Munizioni per lo sparo 
    private Vector3 aimDir;                                                     //Direzione di mira tra il punto da colpire e il punto di spawn

    //Controlli
    public bool isEmpty1;                                                       //Controllo se lo sparo primario non ha munizioni
    public bool isEmpty2;                                                       //Controllo se lo sparo secondario non ha munizioni
    private bool canShoot;                                                      //Controllo se posso sparo
   
    private void Start()
    {
        isEmpty1 = true;
        isEmpty2 = true;
        canShoot = true;
        aim = gameObject.GetComponent<PlayerAim>();
        bulletSpawnPoint = transform.Find("BulletSpawnPoint").gameObject;
    }

    void Update()
    {
        //Direzione del proiettile
        aimDir = (aim.amneryRaycasthit.point - bulletSpawnPoint.transform.position).normalized;               

        //Fuoco Primario
        if (Input.GetKeyDown(KeyCode.Mouse0) && isEmpty1 == false && canShoot == true && PauseController.isGamePaused == false)              //Se ho munzioni e premo sinistro del mouse
        {
            Fire(primaryFire, ref primaryAmmo, ref slot.TXTAmmo1);              //Sparo
            CheckAmmo(primaryAmmo, ref isEmpty1, ref slot.imgEmptySlot1);       //Controllo Munizioni
            StartCoroutine(FireCooldown());                                     //Cooldown sparo
        }

        //Fuoco secondario
        if (Input.GetKeyDown(KeyCode.Mouse1) && isEmpty2 == false && canShoot == true && PauseController.isGamePaused == false)              //Se ho munzioni e premo destro del mouse
        {
            Fire(secondaryFire, ref secondaryAmmo, ref slot.TXTAmmo2);          //Sparo
            CheckAmmo(secondaryAmmo, ref isEmpty2, ref slot.imgEmptySlot2);     //Controllo Munizioni
            StartCoroutine(FireCooldown());                                     //Cooldown sparo
        }
    }

    //Sparo
    private void Fire(GameObject spell, ref int ammo, ref TMP_Text slotAmmo)
    {
        Instantiate(spell, bulletSpawnPoint.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));   //Sparo
        ammo--;                                                                 //-1 munizione
        slotAmmo.SetText(ammo.ToString());                                      //Setto il numero di munizioni nell'interfaccia dello sparo
    }
    
    //Controllo sulle munizioni
    private void CheckAmmo(float ammo, ref bool isEmpty, ref Image slotImage)
    {
        if (ammo == 0)                                                          //Se ho finito le munizioni
        {
            slot.EmptySlot(slotImage);                                          //Tolgo l'immagine della gemma dallo slot dello sparo secondario                                      
            isEmpty = true;                                                     //Setto senza munizioni
        }
    }

    //Cooldown sparo
    IEnumerator FireCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.7f);
        canShoot = true;
    }
}
