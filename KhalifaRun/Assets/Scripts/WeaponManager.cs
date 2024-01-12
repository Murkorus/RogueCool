using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class WeaponManager : MonoBehaviour
{
    [Header("Assignebels")]
    public float pickupDistance;
    public Transform arms;
    public Transform handsHolder;
    public Camera cam;
    
    [Header("Bools")]
    public bool _gotWeapon;
    public bool attacking;
    public bool Swapping_weapons;
    
    
    [Header("Weapons")]
    public GameObject[] close_Weapons;
    public GameObject weponHolder;
    public Weapon_template currentWeapon;
    
    [Header("WeaponStats")]
        public int attackSpeed;
        public int windup;
        public float weight;
    
    [Header("Inventory")]
    public List<Weapon_template> inventory = new List<Weapon_template>();
    public int MaxweaponSlots;
    public GameObject ItemDrop;
    
    
    private Vector3 mousepos;
    private float _distance = 10;
    // Start is called before the first frame update

    private void Update()
    {
        
        close_Weapons = GameObject.FindGameObjectsWithTag("Item_Weapon");

        arms.gameObject.SetActive(_gotWeapon);

        if (Input.GetKeyDown(KeyCode.Alpha1) && inventory.Count > 0 && !attacking && !Swapping_weapons && currentWeapon != inventory[0])
        {
            StartCoroutine(SwapWeapons(inventory[0]));
            updateWeapon(currentWeapon);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.Count > 1 && !attacking && !Swapping_weapons && currentWeapon != inventory[1])
        {
            StartCoroutine(SwapWeapons(inventory[1]));
            updateWeapon(currentWeapon);
            
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            DropWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && _gotWeapon && !attacking && !Swapping_weapons)
        {
            PlayerChargeAttack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && _gotWeapon && !attacking && !Swapping_weapons)
        {
            StartCoroutine(WeaponSpecialMove());
        }
        CheckforWeapons();
        if (inventory.Count == 0)
        {
            currentWeapon = null;
            _gotWeapon = false;
        }

    }
    public void CheckforWeapons()
    {
        if (ClosestWeapon() == null)
        {
            return;
        }
        
        if (Vector2.Distance(transform.position, ClosestWeapon().transform.position) <= pickupDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetWeapon(ClosestWeapon().GetComponent<Item_Weapon>().Weapon);
                Destroy(ClosestWeapon());
            }
        }
    }
    GameObject ClosestWeapon()
    {
        for (int i = 0; i < close_Weapons.Length; i++)
        {
            _distance = Vector2.Distance(transform.position, close_Weapons[i].transform.position);
            
            if (_distance <= pickupDistance)
            {
                return close_Weapons[i];
            }
        }   
        return null;
    }
    public void GetWeapon(Weapon_template weaponn)
        {
            if (inventory.Count > MaxweaponSlots)
            {
                return;
            }
            
            if (inventory.Count != MaxweaponSlots)
            {
                currentWeapon = weaponn;
                inventory.Add(currentWeapon);
            }

            weponHolder.transform.localScale = currentWeapon.size;
            
            if (currentWeapon.TypeWeapon == Weapon_template.Type.Gun)
            {
                weponHolder.GetComponent<SpriteRenderer>().sprite = currentWeapon.Gun_sprite;
                
                Debug.Log("PickUpGun");
            }
            
            if (currentWeapon.TypeWeapon == Weapon_template.Type.Melee)
            {
                weponHolder.GetComponent<SpriteRenderer>().sprite = currentWeapon.Melee_Sprite;
                weight = currentWeapon.Weight;
                windup = currentWeapon.windup;
                
                Debug.Log("PickUpMelee");
            }
            
            _gotWeapon = true;
        }
    public void updateWeapon(Weapon_template weapon)
    {
        weponHolder.transform.localScale = currentWeapon.size;
            
        if (weapon.TypeWeapon == Weapon_template.Type.Gun)
        {
            weponHolder.GetComponent<SpriteRenderer>().sprite = weapon.Gun_sprite;
                
            Debug.Log("PickUpGun");
        }
            
        if (weapon.TypeWeapon == Weapon_template.Type.Melee)
        {
            weponHolder.GetComponent<SpriteRenderer>().sprite = weapon.Melee_Sprite;
            weight = currentWeapon.Weight;
            windup = currentWeapon.windup;
                
            Debug.Log("PickUpMelee");
        }
    }
    public IEnumerator SwapWeapons(Weapon_template swap2weapon)
    {
        
        Swapping_weapons = true;
        var localEulerAngles = handsHolder.localEulerAngles;
        Vector3 weaponPose = new Vector3(localEulerAngles.x,localEulerAngles.y,localEulerAngles.z);
        
        handsHolder.transform.DOLocalRotate(new Vector3(weaponPose.x,weaponPose.y ,-70),0.4f).SetEase(Ease.OutBack);
        
        yield return new WaitForSeconds(0.4f);
        
        handsHolder.transform.DOLocalRotate(new Vector3(weaponPose.x,weaponPose.y ,weaponPose.z),0.4f).SetEase(Ease.OutBack);
        
        currentWeapon = swap2weapon;
        weponHolder.GetComponent<SpriteRenderer>().sprite = currentWeapon.Melee_Sprite;
        weponHolder.transform.localScale = currentWeapon.size;
        
        yield return new WaitForSeconds(0.4f);
        
        Swapping_weapons = false;
    }
    public void DropWeapon()
        {
           if (inventory.Count == 0)
           {
                currentWeapon = null;
               _gotWeapon = false;
               return;
           }
           GameObject Instatiatetdrop = Instantiate(ItemDrop, transform.position,quaternion.identity);
           
           Instatiatetdrop.GetComponent<Item_Weapon>().Weapon = currentWeapon;
           
           inventory.Remove(currentWeapon);
           
           currentWeapon = inventory[0];
           
           updateWeapon(currentWeapon);
            
        }
    private void PlayerChargeAttack()
        {
            attacking = true;
            
            var localEulerAngles = handsHolder.localEulerAngles;
            Vector3 weaponPose = new Vector3(localEulerAngles.x,localEulerAngles.y,localEulerAngles.z);
            Vector3 originalpose_handsholder = new Vector3(localEulerAngles.x,localEulerAngles.y,localEulerAngles.z);
            
            handsHolder.transform.DOLocalRotate(new Vector3(weaponPose.x,weaponPose.y ,weaponPose.z =+ weight * 10),0.4f).SetEase(Ease.OutCirc);

            StartCoroutine(PlayerAttack(originalpose_handsholder));
            
            print("ChargedUp");
        }
    IEnumerator PlayerAttack(Vector3 pose_Handsholder)
        {
            yield return new WaitForSeconds(windup);
            mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 attackdir = (mousepos - transform.position).normalized;
            
            handsHolder.transform.DOLocalRotate(new Vector3(pose_Handsholder.x,pose_Handsholder.y ,pose_Handsholder.z -weight * 10),0.4f).SetEase(Ease.OutBack);
            
            yield return new WaitForSeconds(0.4f);
            
            handsHolder.transform.DOLocalRotate(new Vector3(pose_Handsholder.x,pose_Handsholder.y ,pose_Handsholder.z),0.4f).SetEase(Ease.OutCubic);
            
            weponHolder.transform.DOScale(currentWeapon.size,0.4f).SetEase(Ease.OutCubic);
            
            yield return new WaitForSeconds(0.4f);
            
            attacking = false;
        }
    public IEnumerator WeaponSpecialMove()
         {
             Debug.Log(currentWeapon.Special);
             if (currentWeapon.Special.WeaponObject == SpecialMove_Template.Weapon.linial)
             {
                 weponHolder.transform.DOScale(currentWeapon.Special.newWeaponSize,0.4f).SetEase(Ease.OutBounce);
             }
             if (currentWeapon.Special.WeaponObject == SpecialMove_Template.Weapon.lommelygte)
             {
                 weponHolder.transform.DORotate(new Vector3(0, 0, -90),0.4f).SetEase(Ease.OutCirc);
                 
                 yield return new WaitForSeconds(0.6f);
                 weponHolder.GetComponent<SpriteRenderer>().sprite = currentWeapon.Special.newSprite;

                 Instantiate(currentWeapon.Special.instasiate.gameObject,weponHolder.transform.position,weponHolder.transform.localRotation,weponHolder.transform);
                 weponHolder.transform.DetachChildren();
                 
                 
                 yield return new WaitForSeconds(0.4f);
                 
                 weponHolder.GetComponent<SpriteRenderer>().sprite = currentWeapon.Special.currentSprite;
                 
                 weponHolder.transform.DORotate(new Vector3(0, 0, 0),0.4f).SetEase(Ease.OutCirc);
                 
             }
             if (currentWeapon.Special.WeaponObject == SpecialMove_Template.Weapon.umbrella)
             {
                 weponHolder.GetComponent<SpriteRenderer>().sprite = currentWeapon.Special.newSprite;
                 weponHolder.transform.DOScale(currentWeapon.Special.newWeaponSize,0.4f).SetEase(Ease.OutBounce);
                 weponHolder.transform.DORotate(new Vector3(0, 0, -60),0.4f).SetEase(Ease.OutCirc);
                 
                 yield return new WaitForSeconds(2);
                 
                 weponHolder.GetComponent<SpriteRenderer>().sprite = currentWeapon.Special.currentSprite;
                 weponHolder.transform.DOScale(currentWeapon.size,0.4f).SetEase(Ease.OutBounce);
                 weponHolder.transform.DORotate(new Vector3(0, 0, 0),0.4f).SetEase(Ease.OutCirc);
                 //add knockback  

             }
             if (currentWeapon.Special.WeaponObject == SpecialMove_Template.Weapon.Computer)
             {
                 weponHolder.transform.DOScale(currentWeapon.Special.newWeaponSize,0.4f).SetEase(Ease.OutBounce);
                 
                 yield return new WaitForSeconds(1);
                 
                 weponHolder.transform.DOScale(new Vector3(0,0,0),0.4f).SetEase(Ease.OutCirc);
                 PlayerAttack(weponHolder.transform.position);
                 
                 yield return new WaitForSeconds(0.5f);
                 
                 currentWeapon = null;
                 _gotWeapon = false;
             }
         }
    
        
        
        
}
