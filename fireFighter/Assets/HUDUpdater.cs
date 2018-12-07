using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Effects;

public class HUDUpdater : MonoBehaviour {

    [SerializeField] GameObject fireManager;
    FireLevel fireLevel;
    [SerializeField] WaterHose hose;
    [SerializeField] Text fireTxt;
    [SerializeField] Text damageTxt;
    [SerializeField] Text waterTxt;
    [SerializeField] Text timeTxt;

    // Use this for initialization
    void Start () {
        fireLevel = fireManager.GetComponent<FireLevel>();
        hose.watering += UpdateWater;
        waterTxt.text = hose.water.ToString() + "L";

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void UpdateFire()
    {
        float fire = (float)(fireLevel.FireForLevel - fireLevel.extinguishedFire) / fireLevel.FireForLevel*100f;
        Debug.Log(fire);
        fireTxt.text = fire.ToString("N0") + "%";

    }

    public void UpdateDamage(float damage)
    {

        fireLevel.damage -= damage;
        int dmgRound = (int)fireLevel.damage+1;
        damageTxt.text = dmgRound.ToString()+"%";
    }

    public void UpdateWater()
    {
        hose.water--;
        waterTxt.text = hose.water.ToString()+"L";
    }

    public void UpdateTime()
    {
    }
}
