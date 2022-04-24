using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Waypoint parentWaypoint;

    [SerializeField]
    private float maxHP = 100;
    private float currentHP;
    public Slider hpSlider;

    private Collider mainCollider;
    private Collider[] ragdoll;
    private Rigidbody[] limbs;

    private void Awake()
    {
        parentWaypoint = GetComponentInParent<Waypoint>();
        mainCollider = GetComponent<Collider>();
        ragdoll = GetComponentsInChildren<Collider>();
        limbs = GetComponentsInChildren<Rigidbody>();
        EnableRagdoll(false);
    }
    private void Start()
    {
        currentHP = maxHP;
        UpdateHUD();
    }

    public void TakeDamage(float damage)
    {
        if (currentHP - damage <= 0)
        {
            currentHP = 0;
            EnableRagdoll(true);
            hpSlider.enabled = false;

            parentWaypoint.RemoveEnemyFromList(this.gameObject);
        }
        else
        {
            currentHP -= damage;
            UpdateHUD();
        }
    }

    private void SetHUD(Canvas canvas, Camera camera)
    {
        hpSlider.transform.SetParent(canvas.transform);

        hpSlider.maxValue = maxHP;
        hpSlider.value = maxHP;
    }
    private void UpdateHUD()
    {
        hpSlider.value = currentHP;
    }

    private void EnableRagdoll(bool isEnable)
    {
        foreach (Collider c in ragdoll)
            c.enabled = isEnable;
        foreach (Rigidbody rb in limbs)
            rb.useGravity = isEnable;

        mainCollider.enabled = !isEnable;
        GetComponent<Rigidbody>().useGravity = !isEnable;
        GetComponentInChildren<Animator>().enabled = !isEnable;
    }
}
