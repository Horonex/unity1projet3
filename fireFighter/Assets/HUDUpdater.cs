using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Effects;
using UnityEngine.SceneManagement;

public class HUDUpdater : MonoBehaviour {

    [SerializeField] GameObject fireManager;
    FireLevel fireLevel;
    [SerializeField] WaterHose hose;
    [SerializeField] Text fireTxt;
    [SerializeField] Text damageTxt;
    [SerializeField] Text waterTxt;
    [SerializeField] Text timeTxt;
    [SerializeField] Button restart;
    [SerializeField] Button next;
    [SerializeField] Text endMessage;
    [SerializeField] Text score;
    [SerializeField] GameObject hoseRoot;
    float timeLeft = 120;
   

    public Action lose;

    // Use this for initialization
    void Start () {
        fireLevel = fireManager.GetComponent<FireLevel>();
        hose.watering += UpdateWater;
        waterTxt.text = hose.water.ToString() + "L";
        fireLevel.win += OnWin;
        lose += OnLose;
        Time.timeScale = 1;
        //PlayerPrefs.SetInt("Level", 1);
        //Debug.Log(PlayerPrefs.GetInt("Level"));
    }

    // Update is called once per frame
    void Update () {
        timeLeft -= Time.deltaTime;
        UpdateTime();
	}

    public void UpdateFire()
    {
        float fire = (float)(fireLevel.FireForLevel - fireLevel.extinguishedFire) / fireLevel.FireForLevel*100f;
        fireTxt.text = fire.ToString("N0") + "%";

    }

    public void UpdateDamage(float damage)
    {
        if(fireLevel.damage>0)
        {

            fireLevel.damage -= damage;
            int dmgRound = (int)fireLevel.damage+1;
            damageTxt.text = dmgRound.ToString()+"%";
        }
        else
        {
            //lose
            endMessage.text = "Building Destroyed";
            lose();
            damageTxt.text = "0%";
        }
    }

    public void UpdateWater()
    {
        
        hose.water--;
        waterTxt.text = hose.water.ToString()+"L";
        if(hose.water<=0)
        {
            //lose
            endMessage.text = "Out Of Water";
            lose();
        }
    }

    public void UpdateTime()
    {
        int min = (int)timeLeft/60;
        int sec = (int)timeLeft%60;
        timeTxt.text = min.ToString()+":"+sec.ToString("D2");
        if (timeLeft <= 0)
        {
            //lose
            endMessage.text = "Out Of Time";
            lose();
        }
    }

    public void OnWin()
    {
        
        if(fireLevel.Level<fireLevel.nbOfLevel)
        {
            PlayerPrefs.SetInt("Level",++fireLevel.Level);
            PlayerPrefs.Save();
            int test = PlayerPrefs.GetInt("Level");
            Debug.Log(test);
        }
        float s = (fireLevel.FireForLevel * timeLeft + hose.water) * fireLevel.damage / 100;
        int fs = (int)s;
        score.text = fs.ToString();
        hoseRoot.SetActive(false);
        endMessage.text = "Level Complet";
        endMessage.gameObject.SetActive(true);
        score.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        next.gameObject.SetActive(true);

    }

    public void OnLose()
    {
        hoseRoot.SetActive(false);
        endMessage.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

}
