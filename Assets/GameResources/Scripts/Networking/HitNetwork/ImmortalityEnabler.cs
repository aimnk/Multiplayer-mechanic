using System.Collections;
using GameResources.Scripts.Networking;
using Mirror;
using UnityEngine;

/// <summary>
/// Переключатель неуязвимости игрока
/// </summary>
public class ImmortalityEnabler : NetworkBehaviour
{
    [SerializeField]
    private ImmortalityView immortalityView;

    [SerializeField]
    private PlayerEntity playerEntity;

    [SerializeField]
    private int delayImortality = 3;
    
    private Coroutine coroutineImortality;
    
    /// <summary>
    /// Установить неуязвимость у игрока
    /// </summary>
    public void SetImortality()
    {
        immortalityView.SetOutline(true);
        
        if (playerEntity.isImortality && coroutineImortality == null)
        {
            coroutineImortality = StartCoroutine(DelayImortality());
        }
    }
    
    private IEnumerator DelayImortality()
    {
        yield return new WaitForSeconds(delayImortality);
        CmdDisableImortality();
        coroutineImortality = null;
    }
    
    [Command]
    private void CmdDisableImortality() => DisableImortality();
    
    [ClientRpc]
    private void DisableImortality()
    {
        if (playerEntity.IsImortality)
        {
            immortalityView.SetOutline(false);
            playerEntity.SetImortality(false);
        }
    }
}
