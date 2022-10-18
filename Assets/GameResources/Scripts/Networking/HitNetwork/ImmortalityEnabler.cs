using System.Collections;
using GameResources.Scripts.Networking.Base;
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
    private int delayImmortality = 3;
    
    private Coroutine coroutineImmortality;
    
    /// <summary>
    /// Установить неуязвимость у игрока
    /// </summary>
    public void SetImmortality()
    {
        immortalityView.SetOutline(true);
        
        if (playerEntity.isImortality && coroutineImmortality == null)
        {
            Debug.Log("Start coroutine");
            coroutineImmortality = StartCoroutine(DelayImmortality());
        }
    }
    
    private IEnumerator DelayImmortality()
    {
        yield return new WaitForSeconds(delayImmortality);
        
        CmdDisableImmortality();
        coroutineImmortality = null;
    }
    
    [Command]
    private void CmdDisableImmortality() => DisableImmortality();
    
    [ClientRpc]
    private void DisableImmortality()
    {
        if (playerEntity.IsImortality)
        {
            immortalityView.SetOutline(false);
            playerEntity.SetImortality(false);
        }
    }

    private void OnDisable() => coroutineImmortality = null;
}
