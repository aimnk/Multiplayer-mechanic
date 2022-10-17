using GameResources.Scripts.Input.Actions;
using GameResources.Scripts.Networking;
using Mirror;
using UnityEngine;

/// <summary>
/// Обработчик попаданий по игроку
/// </summary>
public class HitHandler : NetworkBehaviour
{
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
    private void HitToPlayer(PlayerEntity playerEntity)
    {
        playerEntity.DecreaseHealth();

        if (playerEntity.TryGetComponent(out ImmortalityEnabler immortalityEnabler))
        {
            immortalityEnabler.SetImortality();
        }
        else
        {
            Debug.LogError("Не найден компонент " + nameof(ImmortalityEnabler));
        }
    }
}
