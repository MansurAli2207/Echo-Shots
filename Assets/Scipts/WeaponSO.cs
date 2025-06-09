using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Object/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public int Damage = 1;
    public float fireRate = 0.5f;
    public GameObject HitVFXPrefab;
    public bool IsAutomatic = false;

    public bool CanZoom = false;
}
