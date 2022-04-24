using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    private float damage = 25f;

    private Pooler pool;

    private void Start()
    {
        pool = transform.parent.GetComponent<Pooler>();
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyBulletAfterDelay());
    }

    IEnumerator DestroyBulletAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        pool.ReturnPoolObject(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        pool.ReturnPoolObject(this.gameObject);

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }
}
