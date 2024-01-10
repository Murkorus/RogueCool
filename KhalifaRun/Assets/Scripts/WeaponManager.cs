using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] Weapons;
    public GameObject weponHolder;
    public Weapon_template weapon;
    public float pickupDistance;
    public Transform arms;
    public Transform handsHolder;
    public Camera cam;
    
    
    private Vector3 mousepos;
    private float _distance = 10;

    public bool _gotWeapon;
    public bool attacking;
    [Header("WeaponStats")]
        public int attackSpeed;
        public int windup;
        public float weight;
    // Start is called before the first frame update

    private void Update()
    {
        Weapons = GameObject.FindGameObjectsWithTag("Item_Weapon");

        arms.gameObject.SetActive(_gotWeapon);

        if (Input.GetKeyDown(KeyCode.Mouse0) && _gotWeapon)
        {
            PlayerChargeAttack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && _gotWeapon)
        {
            WeaponSpecialMove();
        }
        
        CheckforWeapons();
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
                GetWeapon();
                Destroy(ClosestWeapon());
            }
        }
    }
    GameObject ClosestWeapon()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            _distance = Vector2.Distance(transform.position, Weapons[i].transform.position);
            
            if (_distance <= pickupDistance)
            {
                return Weapons[i];
            }
        }   
        return null;
    }

    // Update is called once per frame
        
        public void GetWeapon()
        {
            weapon = ClosestWeapon().GetComponent<Item_Weapon>().Weapon;

            if (weapon.TypeWeapon == Weapon_template.Type.Gun)
            {
                weponHolder.GetComponent<SpriteRenderer>().sprite = weapon.Gun_sprite;
                
                Debug.Log("PickUpGun");
            }
            
            if (weapon.TypeWeapon == Weapon_template.Type.Melee)
            {
                weponHolder.GetComponent<SpriteRenderer>().sprite = weapon.Melee_Sprite;
                weight = weapon.Weight;
                windup = weapon.windup;
                
                Debug.Log("PickUpMelee");
            }
            _gotWeapon = true;
        }

        private void PlayerChargeAttack()
        {
            attacking = true;
            
            var localEulerAngles = handsHolder.localEulerAngles;
            Vector3 weaponPose = new Vector3(localEulerAngles.x,localEulerAngles.y,localEulerAngles.z);
            Vector3 originalpose = new Vector3(localEulerAngles.x,localEulerAngles.y,localEulerAngles.z);
            
            handsHolder.transform.DOLocalRotate(new Vector3(weaponPose.x,weaponPose.y ,weaponPose.z =+ weight * 10),0.4f).SetEase(Ease.OutCirc);

            StartCoroutine(PlayerAttack(originalpose));
            
            print("ChargedUp");
        }
        
         IEnumerator PlayerAttack(Vector3 pose)
        {
            yield return new WaitForSeconds(windup);
            mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 attackdir = (mousepos - transform.position).normalized;
            
            handsHolder.transform.DOLocalRotate(new Vector3(pose.x,pose.y ,pose.z -weight * 10),0.4f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.4f);
            handsHolder.transform.DOLocalRotate(new Vector3(pose.x,pose.y ,pose.z),0.4f).SetEase(Ease.OutCubic);

            
            weponHolder.transform.DOScale(new Vector3(0.5f,0.5f,0.5f),0.4f).SetEase(Ease.OutCubic);
            attacking = false;
        }

         public void WeaponSpecialMove()
         {
             Debug.Log(weapon.Special);
             if (weapon.Special.WeaponObject == SpecialMove_Template.Weapon.linial)
             {
                 weponHolder.transform.DOScale(weapon.Special.newWeaponSize,0.4f).SetEase(Ease.OutBounce);
             }
             
             if (weapon.Special.WeaponObject == SpecialMove_Template.Weapon.lommelygte)
             {
                 
             }
         }
        
        
        
}
