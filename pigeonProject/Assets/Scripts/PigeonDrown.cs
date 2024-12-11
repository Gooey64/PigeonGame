using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonDrown : MonoBehaviour
{
    public Transform wallBottom;
    private GameObject player;
    private HealthManager healthManager;
    private bool damage = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
        healthManager = player.GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= wallBottom.position.y + 1) {
            // Debug.Log("drown");
            if (healthManager != null && !damage)
            {
                StartCoroutine(drowning());
            }
        }
    }

    IEnumerator drowning() {
        damage = true;
        yield return new WaitForSeconds(0.5f);
        healthManager.TakeDamage(10);
        damage = false;
    }
}
