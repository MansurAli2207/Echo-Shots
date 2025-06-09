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

        HandleShoot();
        HandleZoom();

    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }
        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.weaponSO = weaponSO;
    }
    void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;
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

    void HandleZoom()
    {
        if (!weaponSO.CanZoom) return;
        if (starterAssetsInputs.Zoom)
        {
            Debug.Log("Zooming In");
        }
        else
        {
            Debug.Log("Zoomed Out");
        }
    }
}
