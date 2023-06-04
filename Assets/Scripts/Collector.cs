using HmsPlugin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HuaweiMobileServices.Ads;
using HuaweiMobileServices.Utils;

public class Collector : MonoBehaviour
{
    GameObject mainCube;
    AdsManager _adsManager;
    public Animator m_animator;
    public GameObject cubeExample;
    public GameObject ParticleEffect;
    int height;
    int cumScore;
    float time;
    ScoreManager _scoreManager;
    void Start()
    {
        mainCube = GameObject.Find("mainCube");
        _adsManager = FindObjectOfType<AdsManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        HMSAdsKitManager.Instance.ShowBannerAd();
    }

    // Update is called once per frame
    void Update()
    {
        mainCube.transform.position = new Vector3(transform.position.x, height + 1, transform.position.z);
        this.transform.localPosition = new Vector3(0, -height, 0);
    }
    public void inscreaseHeight()
    {
        height--;
        Debug.Log("height increased"+height);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube" && other.gameObject.GetComponent<Cube>().getCollected() == false)
        {
            IncreaseHeight(other.gameObject);
        }
        if(other.gameObject.tag == "Finnish")
        {
            cumScore += height;
            FindObjectOfType<CharacterController>().forwardSpeed = 0;
            Invoke("loadScenewDelay", 2f);
        }
        if (other.gameObject.tag == "Coin")
        {
            _scoreManager.SetScore(1);
            Debug.Log(_scoreManager.Score);
            Destroy(other.gameObject);
        }
    }

    public int getHigh()
    {
        return height;
    }
    public int getCumScore()
    {
        return cumScore;
    }
    
    void loadScenewDelay()
    {
        _adsManager.ShowInterstitialAd();
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            SceneManager.LoadScene("Level1");
        }
    }
    public void GetReward()
    {
        //HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
        HMSAdsKitManager.Instance.ShowRewardedAd();
    }

    void IncreaseHeight(GameObject obj)
    {
        m_animator.SetTrigger("isJumpp");
        height += 1;
        ParticleEffect.GetComponent<ParticleSystem>().Play();
        obj.GetComponent<Cube>().setCollected();
        obj.GetComponent<Cube>().setIndex(height);
        obj.transform.parent = mainCube.transform;
        //Debug.Log("height decreased " + height + "  " + time);
    }
    public void OnRewarded()
    {
        AddBlock(5);
    }

    public void AddBlock(int value)
    {
        for(int i = 0; i < value; i++)
        {
            GameObject tempObj = Instantiate(cubeExample);
        }
    }

    public void AddBlockwCoin()
    {
        if (_scoreManager.GetScore() > 5)
        {
            AddBlock(1);
            _scoreManager.SetScore(-5);
        }
    }
}
