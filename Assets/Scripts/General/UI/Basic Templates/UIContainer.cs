using UnityEngine;

namespace AutoChessRPG
{
    public class UIContainer : MonoBehaviour
    {
        protected int resolutionWidth;
        protected int resolutionHeight;
        
        public void ToggleActiveState() => gameObject.SetActive(!gameObject.activeSelf);

        public virtual void UpdateResolution()
        {
            // resolution stuff I guess not sure
        }
    }
}
