using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireLevel : MonoBehaviour {

    [SerializeField] public GameObject[] fires;
    [SerializeField] HUDUpdater canvas;
    [SerializeField] WaterHose hose;
    public int FireForLevel;
    int activatedFire;
    public int Level=6;
    int nbOfLevel=6;
    int StartingFire;
    public int extinguishedFire=0;

    public float damage=100f;

    // Use this for initialization
    void Start () {
        FireForLevel = Level + 3;
        StartingFire = FireForLevel / 2;
		foreach(var f in fires)
        {
            f.GetComponent<Extinguishing>().extinguished += OnFireExtinguished;
            f.SetActive(false);
        }
        for(int i=0;i<StartingFire;i++)
        {
            ActivateFire();
        }
        hose.water = 200 * FireForLevel + 400;

        FireGameplay();

    }
	
    void FireGameplay()
    {
        if (activatedFire<FireForLevel)
        {
            float a = nbOfLevel + 1f - Level;
            float b = 2f * (nbOfLevel + 1 - Level) + 1;
            float rTime = Random.Range(a, b);
            ActivateFire();
            Invoke("FireGameplay", rTime);
        }
        
    }

    void ActivateFire()
    {
        int rIndex = Random.Range(0, fires.Length);
        if(!fires[rIndex].active)
        {
            fires[rIndex].SetActive(true);
            activatedFire++;
        }
    }

    void OnFireExtinguished()
    {
        extinguishedFire++;
        if(extinguishedFire>=FireForLevel)
        {
            //Win
            Debug.Log("win");
        }
        canvas.UpdateFire();
    }

	// Update is called once per frame
	void Update () {
		
	}

}
