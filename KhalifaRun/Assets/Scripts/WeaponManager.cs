using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class WeaponManager : MonoBehaviour
{
    public Transform arms;
    public GameObject weponHolder;
    public Weapon_template weapon;
    public GameObject[] Weapons;
    public Camera cam;
    
    public float pickupDistance;
    
    private Vector3 mousepos;
    private float _distance = 10;

    public bool _gotWeapon;

    [Header("WeaponStats")]
        public int attackSpeed;
        public int windup;
        public int force;
    // Start is called before the first frame update

    private void Update()
    {
        Weapons = GameObject.FindGameObjectsWithTag("Item_Weapon");
        
        if (!_gotWeapon)
        {
            arms.gameObject.SetActive(false);}
        else
        {
            arms.gameObject.SetActive(true);}
        
        
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
            if (Input.GetKey(KeyCode.Mouse0) && _gotWeapon)
            {
                PlayerChargeAttack();
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
            }
            
            if (weapon.TypeWeapon == Weapon_template.Type.Melee)
            {
                weponHolder.GetComponent<SpriteRenderer>().sprite = weapon.Melee_Sprite;
            }
            _gotWeapon = true;
            Debug.Log("PickUpWeapon");
        }

        private void PlayerChargeAttack()
        {
            print("Attack");
            Vector3 weaponPose = new Vector3(arms.localEulerAngles.x,arms.localEulerAngles.y,arms.localEulerAngles.z);
            
            arms.transform.DOLocalRotate(new Vector3(weaponPose.x,weaponPose.y,weaponPose.z + force ),0.4f).SetEase(Ease.Flash);
            Invoke(nameof(PlayerAttack),windup);
        }
        
        public void PlayerAttack()
        {
            mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 attackdir = (mousepos - transform.position).normalized;
        }
        
        
        
}
