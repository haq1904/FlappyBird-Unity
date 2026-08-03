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

    public override int GetSelectedSkinId()
    {
        return PlayerPrefs.GetInt("SelectedSkinId", 0);
    }

    public override void SetSelectedSkinId(int skinID)
    {
        PlayerPrefs.SetInt("SelectedSkinId", skinID);
        PlayerPrefs.Save();
    }

    public override int GetSelectedShopItemId()
    {
        return PlayerPrefs.GetInt("SelectedShopItemId", 0);
    }

    public override void SetSelectedShopItemId(int itemID)
    {
        PlayerPrefs.SetInt("SelectedShopItemId", itemID);
        PlayerPrefs.Save();
    }

    public override bool IsItemIdUnlocked(int skinID)
    {
        if (skinID == 0) return true;

        return PlayerPrefs.GetInt("UnlockedItemId_" + skinID, 0) == 1;
    }

    public override void UnlockItemId(int skinID)
    {
        PlayerPrefs.SetInt("UnlockedItemId_" + skinID, 1);
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
