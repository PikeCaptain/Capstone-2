using InfimaGames.LowPolyShooterPack;
using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public Character starterAssetsInputs;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualFireInput(bool virtualJumpState)
        {
            starterAssetsInputs.FireInput(virtualJumpState);
        }

        public void VirtualPauseInput(bool virtualJumpState)
        {
            starterAssetsInputs.PauseInput(virtualJumpState);
        }

        public void VirtualReloadInput(bool virtualJumpState)
        {
            starterAssetsInputs.ReloadInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }
        
    }

}
