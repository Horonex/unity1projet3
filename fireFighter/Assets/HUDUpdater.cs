using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdater : MonoBehaviour {

    [SerializeField] GameObject fireManager;
    FireLevel fireLevel;
    [SerializeField] Text fireTxt;
    [SerializeField] Text damageTxt;
    [SerializeField] Text waterTxt;
    [SerializeField] Text timeTxt;

    // Use this for initialization
    void Start () {
        fireLevel = fireManager.GetComponent<FireLevel>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateFire()
    {

    }

    public void UpdateDamage(float damage)
    {

        fireLevel.damage -= damage;
        int dmgRound = (int)fireLevel.damage+1;
        damageTxt.text = dmgRound.ToString()+"%";
    }

    public void UpdateWater()
    {

    }

    public void UpdateTime()
    {
    }
}
