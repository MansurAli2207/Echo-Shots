using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    float timeSinceLastShot = 0f;


    Animator animator;
    StarterAssetsInputs starterAssetsInputs;

    Weapon currentWeapon;


    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();

    }
    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        HandleShoot();

    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        Debug.Log("Player Picked Up a : " + weaponSO.name);
    }
    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;
        if (timeSinceLastShot >= weaponSO.fireRate)
        {
            currentWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f;
        }

        if (!weaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }




    }
}
