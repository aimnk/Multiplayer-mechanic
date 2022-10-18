using System;
using GameResources.Scripts.Input.Actions;
using GameResources.Scripts.Networking.Base;
using Mirror;
using UnityEngine;

/// <summary>
/// Обработчик попаданий по игроку
/// </summary>
public class HitHandler : NetworkBehaviour
{
    /// <summary>
    /// Событие - игрок нанес урон
    /// </summary>
    public event Action<PlayerEntity> onHit = delegate { };
    
     [SerializeField] 
    private DashAbility dashAbility;

    private Coroutine delayImortality;
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        dashAbility.onHitOtherPlayer += CmdOnHit;
    }
    
    public override void OnStopClient()
    {
        base.OnStopClient();
        dashAbility.onHitOtherPlayer -= CmdOnHit;
    }
    
    [Command]
    private void CmdOnHit(PlayerEntity playerEntity) => HitToPlayer(playerEntity);

    [ClientRpc]
    private void HitToPlayer(PlayerEntity otherPlayerEntity)
    {
        if (otherPlayerEntity.isImortality)
            return;
        
        otherPlayerEntity.DecreaseHealth();
        
        if (otherPlayerEntity.TryGetComponent(out ImmortalityEnabler immortalityEnabler))
        {
            immortalityEnabler.SetImmortality();
        }
        else
        {
            Debug.LogError("Не найден компонент " + nameof(ImmortalityEnabler));
        }
        
        if (gameObject.TryGetComponent(out PlayerEntity playerEntity))
        {
            OnHit(playerEntity);
        }
    }
    
    private void OnHit(PlayerEntity playerEntity) => onHit.Invoke(playerEntity);

}
