using GameResources.Scripts.Input.Base;

namespace GameResources.Scripts.Base
{
   public class Game
   {
      public static IInputService InputService;
      public Game()
      {
         InputService = new SimpleInput();
      }
   }
}
