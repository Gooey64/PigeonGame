using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PackageHandler : MonoBehaviour
{

    public int packagePicked = 0;
    public delegate void PackagePicked(GameObject gameObject);
    public event PackagePicked OnPackagePicked;
    public KeyCode dropKey = KeyCode.Space;
    public GameObject packagePrefab;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        OnPackagePicked += this.UpdatePackagePicked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dropKey)) {
            if (this.packagePicked > 0) {
                DropPackage();
            }
        }
    }

    public void UpdatePackagePicked(GameObject _) {
        this.packagePicked++;
    }

    void DropPackage() {
        this.packagePicked--;
        GameObject instance = GameObject.Instantiate(packagePrefab);
        instance.transform.position = player.transform.position;
    }

    public void PickUpPackage(GameObject obj) {
        this.OnPackagePicked?.Invoke(obj);
    }
}
