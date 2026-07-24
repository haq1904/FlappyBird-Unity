using UnityEngine;

public abstract class DataManagerService : MonoBehaviour
{
    public abstract int GetBestScore();
    public abstract void SaveScore(int currentScore);
    
    public abstract int GetCoins();
    public abstract void AddCoins(int amount);
    public abstract bool SpendCoins(int amount);
    
    public abstract int GetSelectedSkin();
    public abstract void SetSelectedSkin(int skinID);
    
    public abstract bool IsSkinUnlocked(int skinID);
    public abstract void UnlockSkin(int skinID);
    
    public abstract float GetVolume();
    public abstract void SetVolume(float volume);
}
