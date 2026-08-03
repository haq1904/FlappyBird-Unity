using UnityEngine;

public abstract class DataManagerService : MonoBehaviour
{
    public abstract int GetBestScore();
    public abstract void SaveScore(int currentScore);


    public abstract float GetCoins();
    public abstract void AddCoins(float amount);
    public abstract bool SpendCoins(float amount);


    public abstract int GetSelectedSkinId();
    public abstract void SetSelectedSkinId(int skinID);

    public abstract int GetSelectedShopItemId();
    public abstract void SetSelectedShopItemId(int itemID);

    public abstract bool IsItemIdUnlocked(int skinID);
    public abstract void UnlockItemId(int skinID);


    public abstract float GetVolume();
    public abstract void SetVolume(float volume);
}
