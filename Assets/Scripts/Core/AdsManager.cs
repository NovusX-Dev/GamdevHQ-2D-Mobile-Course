using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string _gameID = "4176203";
    private string _rewardedVideo = "rewardedVideo";
    bool _testMode = true;


    private void OnEnable()
    {
        Advertisement.AddListener(this);
    }

    private void Start()
    {
       Advertisement.Initialize(_gameID, _testMode);
    }

    private void OnDisable()
    {
       Advertisement.RemoveListener(this);
    }

    public void ShowRewardedVideo()
    {
        if (Advertisement.IsReady(_rewardedVideo))
        {
            Advertisement.Show(_rewardedVideo);
        }
        else
        {
            Debug.Log("Rewarded Video is not ready, please try again later!");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Rewarded Ad is ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogWarning("The ad did not finish due to an error.");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Rewarded Ad started!");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                PlayerStats.Instance.AddDiamonds(100);
                SavingSystem.Instance.SaveCurrency();
                UIManager.Instance.UpdateShopDiamonds(PlayerStats.Instance.GetDiamondAmount());
                break;
            case ShowResult.Failed:
                Debug.LogWarning("The ad did not finish due to an error.");
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("The ad was skipped, you will not be rewarded");
                break;
        }
    }

   
}

