using UnityEngine;

namespace Assets.Scripts
{
    public class GameAssets: MonoBehaviour
    {
        private static GameAssets instance;

        public static GameAssets Instance
        {
            get
            {
                if (instance == null)
                    instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
                return instance;
            }
        }

        public GameObject CombatText;

        public GameObject pfStoryScreen;
        public GameObject pfRewardScreen;
        public GameObject pfSwapScreen;
    }
}
