using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;
using UnityStandardAssets.Utility;

public class Extinguishing : MonoBehaviour {
	public float multiplier = 1f;
	[SerializeField] private float reduceFactor = 0.8f;
	[SerializeField] private GameObject checkbox;
	private AudioSource audioS;
    bool isExtinguished=false;
    [SerializeField]  float damageFactor=0.5f;
    [SerializeField] GameObject firePrefab;
    [SerializeField] GameObject fire;


    public Action extinguished;

    [SerializeField] HUDUpdater canvas;

	// Use this for initialization
	void Start () {

		checkbox.SetActive(false);
		ParticleSystemMultiplier SysMul = GetComponent<ParticleSystemMultiplier>();
		multiplier = SysMul.multiplier;
        reduceFactor *= multiplier;
		audioS = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Extinguish()
    {
        multiplier -= reduceFactor;
        audioS.volume -= reduceFactor;
        var systems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem system in systems)
        {
            ParticleSystem.MainModule mainModule = system.main;
            mainModule.startSizeMultiplier *= reduceFactor;
            mainModule.startSpeedMultiplier *= reduceFactor;
            //mainModule.startLifetimeMultiplier *= Mathf.Lerp(multiplier, 1, 0.5f);
            //system.Clear();
            system.Play();
            Invoke("ResetFire", 10);
        }
        if (multiplier <= 0&&!isExtinguished)
        {
            isExtinguished = true;
            GetComponent<ParticleSystemDestroyer>().Stop();
            checkbox.SetActive(true);
            checkbox.transform.parent = null;
            extinguished();
        }
    }

    private void Update()
    {

        if(gameObject.active&&!isExtinguished)
        {
            canvas.UpdateDamage(Time.deltaTime * damageFactor);
        }
    }

    private void ResetFire()
    {
        GameObject f = Instantiate(firePrefab,fire.transform);
        f.transform.parent = fire.transform.parent;
        //Destroy(fire);
        fire = f;
        
    }

}
