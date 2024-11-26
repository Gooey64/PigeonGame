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
    public KeyCode dropKey = KeyCode.E;
    public GameObject packagePrefab;
    public Transform initPos;
    public Transform pigeonBeakPos;
    public Transform pigeonFeetPos;
    public GameObject packageOnPigeon;
    public GameObject LevelFinishedPanel;
    [SerializeField] AudioClip PickAudio;
    [SerializeField] AudioClip DropAudio;

    private GameObject player;
    private bool isGrounded = true;

    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        this.pigeonBeakPos = player.transform.Find("pigeonBeak");
        this.pigeonFeetPos = player.transform.Find("pigeonFeet");
        this.packageOnPigeon = player.transform.Find("package").gameObject;
        // update the variable when pick up pakcage
        OnPackagePicked += this.UpdatePackagePicked;
        // show the package on the pigeon
        OnPackagePicked += this.AcitvePackageOnPigeon;
        // when package delivered, finish the level
        OnPackageDelivery += this.ShowLevelFishedPanel;
        this.InitPackage(gameObject);
    }

    void Update()
    {
        if (Input.GetButtonDown("Action")) {
            if (this.packagePicked > 0) {
                DropPackage();
            }
        }
        if (this.packageOnPigeon.activeSelf) {
            if (isGrounded) {
                this.packageOnPigeon.transform.position = pigeonBeakPos.transform.position;
            }
            else {
                this.packageOnPigeon.transform.position = pigeonFeetPos.transform.position;
            }
        }
    }

    void UpdatePackagePicked(GameObject _) {
        this.packagePicked++;
    }

    void DropPackage() {
        this.packagePicked--;
        gameObject.GetComponent<AudioSource>().PlayOneShot(DropAudio);
        GenerateNewPackage(player.transform);
        this.packageOnPigeon.SetActive(false);
    }

    public void PickUpPackage(GameObject obj) {
        this.OnPackagePicked?.Invoke(obj);
        gameObject.GetComponent<AudioSource>().PlayOneShot(PickAudio);
        Debug.Log("package picked up!");
    }

    void GenerateNewPackage(Transform transform) {
        GameObject instance = GameObject.Instantiate(packagePrefab);
        instance.transform.position = transform.position;
    }

    void InitPackage(GameObject _) {
        GenerateNewPackage(initPos.transform);
    }

    public void DeliverPackage(GameObject obj) {
        this.OnPackageDelivery?.Invoke(obj);
        Debug.Log("package delivered!");
    }

    public void ShowLevelFishedPanel(GameObject _) {
        LevelFinishedPanel.SetActive(true);
    }

    public void AcitvePackageOnPigeon(GameObject _) {
        this.packageOnPigeon.SetActive(true);
    }

}