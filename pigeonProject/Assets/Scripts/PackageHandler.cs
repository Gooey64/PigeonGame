using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class PackageHandler : MonoBehaviour
{

    public int packagePicked = 0;
    public delegate void PackageEvents(GameObject gameObject);
    public event PackageEvents OnPackagePicked; // event called when package picked up
    public event PackageEvents OnPackageDelivery;   // event called when package delivered
    public KeyCode dropKey = KeyCode.Space;
    public GameObject packagePrefab;
    public int score = 0;
    public Transform initPos;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        // update the variable when pick up pakcage
        OnPackagePicked += this.UpdatePackagePicked;
        // when package delivered, update score and init another package
        // TODO: should be modified later with new package generation logics
        OnPackageDelivery += this.UpdateScore;
        OnPackageDelivery += this.initPackage;
        this.initPackage(gameObject);
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

    void UpdatePackagePicked(GameObject _) {
        this.packagePicked++;
    }

    void DropPackage() {
        this.packagePicked--;
        GenerateNewPackage(player.transform);
    }

    public void PickUpPackage(GameObject obj) {
        this.OnPackagePicked?.Invoke(obj);
        Debug.Log("package picked up!");
    }

    void UpdateScore(GameObject _) {
        this.score++;
    }

    void GenerateNewPackage(Transform transform) {
        GameObject instance = GameObject.Instantiate(packagePrefab);
        instance.transform.position = transform.position;
    }

    void initPackage(GameObject _) {
        GenerateNewPackage(initPos.transform);
    }

    public void DeliverPackage(GameObject obj) {
        this.OnPackageDelivery?.Invoke(obj);
        Debug.Log("package delivered!");
    }
}
