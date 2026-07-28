using UnityEngine;

public class DataManager : DataManagerService
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore", 0);
    }

    public override void SaveScore(int currentScore)
    {
        if (currentScore > GetBestScore())
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            PlayerPrefs.Save();
        }
    }

    public override float GetCoins()
    {
        return PlayerPrefs.GetFloat("Coins", 0);
    }

    public override void AddCoins(float amount)
    {
        PlayerPrefs.SetFloat("Coins", GetCoins() + amount);
        PlayerPrefs.Save();
    }

    public override bool SpendCoins(float amount)
    {
        float currentCoins = GetCoins();
        if (currentCoins >= amount)
        {
            PlayerPrefs.SetFloat("Coins", currentCoins - amount);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    public override int GetSelectedSkin()
    {
        return PlayerPrefs.GetInt("SelectedSkin", 0);
    }

    public override void SetSelectedSkin(int skinID)
    {
        PlayerPrefs.SetInt("SelectedSkin", skinID);
        PlayerPrefs.Save();
    }

    public override bool IsSkinUnlocked(int skinID)
    {
        if (skinID == 0) return true;

        return PlayerPrefs.GetInt("UnlockedSkin_" + skinID, 0) == 1;
    }

    public override void UnlockSkin(int skinID)
    {
        PlayerPrefs.SetInt("UnlockedSkin_" + skinID, 1);
        PlayerPrefs.Save();
    }

    public override float GetVolume()
    {
        return PlayerPrefs.GetFloat("Volume", 1f);
    }

    public override void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}
