using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class equipweapon : MonoBehaviour
{
    public List <itemclass> weapon = new List<itemclass>(1);
    public bool isequiped = false;
    public Transform guncontainer;
    public Text peluruteks;
    public int currentammo;
    public bool canshoot = false;
    public bool canreload = false;
    public GameObject peluru;

    private void Update()
    {
        
        if(weapon[0] != null)
        {
            if (currentammo <= 0)
                canshoot = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "gun")
        {

            if (weapon[0] == null)
            {
                weapon[0] = Resources.Load<itemclass>(other.gameObject.name);
                GameObject go = Instantiate(weapon[0].gun, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                go.transform.parent = guncontainer;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.Euler(Vector3.zero);
                go.transform.localScale = Vector3.one;
                go.GetComponent<Collider>().enabled = false;
                currentammo = Mathf.Clamp(weapon[0].currentmagazine, 0, weapon[0].stockmagazine);
                peluruteks.text = currentammo + "/" + weapon[0].stockmagazine;
                canshoot = true;
                Destroy(other.gameObject);
                isequiped = true;
            }
            else
                return;
        }
    }

    public void launch(int amount)
    {
        //Animator anim = guncontainer.transform.GetChild(0).GetComponent<Animator>();
        //Rigidbody weaprb = guncontainer.transform.GetChild(0).GetComponent<Rigidbody>();
        if (canshoot)
        {
            if (amount < 0)
            {
                GameObject peluruGameObject = Instantiate(peluru, guncontainer.transform.GetChild(0).GetChild(0).transform.position, guncontainer.transform.GetChild(0).GetChild(0).transform.rotation);
                peluruGameObject.GetComponent<Rigidbody>().AddForce(guncontainer.transform.GetChild(0).GetChild(0).transform.forward * 100, ForceMode.Impulse);
                //weaprb.AddTorque(0,20,0,ForceMode.Force);
                //anim.SetTrigger("shot");
                currentammo = Mathf.Clamp(currentammo + amount, 0, weapon[0].stockmagazine);
                peluruteks.text = currentammo + "/" + weapon[0].stockmagazine;
                canreload = true;
            }
        }
    }
    public IEnumerator Reload()
    {
        canshoot = false;
        yield return new WaitForSeconds(3);
        weapon[0].stockmagazine += currentammo - weapon[0].maxmagazine;
        currentammo = Mathf.Clamp(weapon[0].maxmagazine, 0, weapon[0].stockmagazine);
        peluruteks.text = currentammo + "/" + weapon[0].stockmagazine;
        canreload = false;
        canshoot = true;
    }

    public void unequipingweap()
    {
        Destroy(guncontainer.GetChild(0).gameObject);
        weapon[0] = null;
        isequiped = false;
        canshoot = false;
        canreload = false;
    }
}
