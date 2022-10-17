using GameResources.Scripts.Networking;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// VFX эффект способности рывка
/// </summary>
public class DashEffect : MonoBehaviour
{
   [SerializeField] 
   private GameObject PrefabParticleDash;
   
   private ObjectPool<GameObject> particleDashPool;
   
   private GameObject currentDash;

   private PlayerEntity playerEntity;

   private void Start()
   {
       playerEntity = GetComponentInParent<PlayerEntity>();
       
       InitPool();
       
       if (playerEntity.InputService != null)
           playerEntity.InputService.onDashDown += ShowDashEffect;
   }

   private void OnDestroy()
   {
       if (playerEntity.InputService != null)
           playerEntity.InputService.onDashDown -= ShowDashEffect;
   }

   private void InitPool()
   {
       particleDashPool = new ObjectPool<GameObject>(
           () => Instantiate(PrefabParticleDash, transform),
           (obj) => obj.SetActive(true),
           (obj) => obj.SetActive(false),
           (obj) => Destroy(obj)
       );
   }

   private void ShowDashEffect()
   {
       if (playerEntity.InputService.MoveDirection.sqrMagnitude < Mathf.Epsilon)
           return;
       ;
       if (currentDash != null)
           particleDashPool.Release(currentDash);

       currentDash = particleDashPool.Get();
   }
}
