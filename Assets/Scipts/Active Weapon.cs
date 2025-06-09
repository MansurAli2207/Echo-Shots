using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] GameObject zoomVignette;
     float timeSinceLastShot = 0f;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    Weapon currentWeapon;
    float defaultFov;
    float defaultRotationSpeed;
    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFov = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;

    }
    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        
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

           playerFollowCamera.m_Lens.FieldOfView = weaponSO.zoomAmount;
           firstPersonController.ChangeRotationSpeed(weaponSO.zoomRotationSpeed);
           zoomVignette.SetActive(true);

        }
        else
        {
             playerFollowCamera.m_Lens.FieldOfView = defaultFov;
             zoomVignette.SetActive(false);
             firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
