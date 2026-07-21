using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //public float lifeTime = 3f;
    public float damage = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameObject.name.EndsWith("(Clone)"))
        {
            Destroy(gameObject, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && gameObject.name.EndsWith("(Clone)") /*&& Time.time >= nextFireTime*/)
        {
            //nextFireTime = Time.time + fireRate;
            Destroy(gameObject);
        }
    }

    void onCollisionEnter(Collision collision)
    {
        /*
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }*/
        Destroy(gameObject);


    }
}
