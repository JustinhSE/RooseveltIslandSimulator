using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventsHandler : MonoBehaviour
{
    TrafSpawner trafspawner;
    private GameObject player;
    private bool eventHappened; 
    

    [SerializeField]
    private string eventName;
    //[SerializeField]
    //private string targetObjectName; //XE_Rigged(Clone) is the name of the spawned car
    [SerializeField]
    private float targetObjectApproachDistance;
    [SerializeField]
    private float targetObjectAwayDistance;



    // Use this for initialization
    private void Awake()
    {
        //targetObjectName = "XE_Rigged(Clone)";
    }
    void Start()
    {
        eventHappened = false;
        player = _GetTargetObeject();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = _DistanceDetection();
        if (eventHappened == false && dist < targetObjectApproachDistance)
        {
            StartCoroutine(StartEvent(eventName));
            eventHappened = true;
        }
    }

    private GameObject _GetTargetObeject (){
        player = FindObjectOfType<VehicleController>().gameObject;
        return player;
    }

    private float _DistanceDetection(){
        player = FindObjectOfType<VehicleController>().gameObject;
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        return dist;
    }


    private void _Jaywalker(GameObject player){
        //load and play audio
        LoadAndPlaySoundToObject(player, "jaywalker");
        Debug.Log("We juust started the JayWalk script. Shpould happen when crossing a red light!");
        //load game object
        LoadSpawnObject(this.gameObject.transform, "man");
    }

    private void _ArrivingDestination(GameObject player){
        LoadAndPlaySoundToObject(player, "arrivingDestination");
    }

    private void _CarCutInLane(GameObject player)
    {
        LoadAndPlaySoundToObject(player, "carCutInLane");
        GameObject carCutIn = LoadSpawnObject(player.transform, "cutInCar", new Vector3(30,0,10));
        StartCoroutine(CutInLane(carCutIn.transform));
        //trafspawner.Spawn();

    }

    private void _RedLight(){

    }

    private void _Market(GameObject player){
        LoadAndPlaySoundToObject(player, "approachMarket");
    }
    private void _Rain(GameObject player)
    {
        LoadAndPlaySoundToObject(player, "rain");
        GameObject rain = LoadSpawnObject(this.gameObject.transform, "Rain");
    }
    private void _Stadium(GameObject player)
    {
        LoadAndPlaySoundToObject(player, "stadium");
    }

    private void LoadAndPlaySoundToObject(GameObject player, string soundFile)
    {
        var audioClip = Resources.Load<AudioClip>("Audios/" + soundFile);
        AudioSource audioSource = player.AddComponent<AudioSource>() as AudioSource;
        player.GetComponent<AudioSource>().PlayOneShot(audioClip, 1.0f);
    }

    private GameObject LoadSpawnObject(Transform eventLocationTransform, string prefabName, Vector3 positionAway = new Vector3())
    {
        GameObject prefab = Resources.Load("prefabs/" + prefabName) as GameObject;
        GameObject prefabInstantiate = Instantiate(prefab, eventLocationTransform.position + positionAway, eventLocationTransform.rotation);
        return prefabInstantiate;
    }

    //Coroutines

    IEnumerator StartEvent(string eventName)
    {
        switch (eventName)
        {
            case "Jaywalker":
                _Jaywalker(player);
                break;
            case "ArrivingDestination":
                _ArrivingDestination(player);
                break;
            case "CarCutInLane":
                _CarCutInLane(player);
                break;
            case "RedLight":
                _RedLight();
                break;
            case "ShortOfPower":
                break;
            case "Market":
                _Market(player);
                break;
            case "Rain":
                _Rain(player);
                break;
            case "Stadium":
                _Stadium(player);
                break;
            case "None":
                Debug.Log("no event");
                break;
            
        }
        print(eventName + " is triggered");
        yield return new WaitForEndOfFrame();
    }


    IEnumerator CutInLane(Transform cutInCarTransform){
        var start_pos = cutInCarTransform.position;
        var end_pos = start_pos + new Vector3 (40,0,-10);
        float step = (100f / (start_pos - end_pos).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while(t <= 1.0f){
            t += step;
            cutInCarTransform.position = Vector3.Lerp(start_pos, end_pos, t);
            yield return new WaitForEndOfFrame();
        }
        cutInCarTransform.position = end_pos;
    }



}
