using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Assets.Scripts.Core;

namespace Assets.Scripts
{
    public class Scenarios : MonoBehaviour
    {
        public ScreenBase curScreen;
        public ScreenBase gameOverScreen;
        public List<ActorBase> startingCaravan;

        // Screen
        public CaravanManager caravanManager;
        public GameController gameController;

        // Audio
        public AudioClip ambientAudio;
        public AudioClip encounterAudio;
        public AudioSource audioSource;

        public void Start()
        {
            caravanManager.InitializeCaravan(startingCaravan
               .Select(item => Instantiate(item))
               .ToList());
            this.HandleCurrentScreenChange();
            PlayAmbient();
        }

        public void OnStoryScreenContinue()
        {
            if (curScreen is EncounterScreen)
            {
                PlayEncounter();
                var screen = curScreen as EncounterScreen;
                this.gameController.StartEncounter(screen.EnemyPack
                    .Select(item => EnemyBase.Create(item.Name))
                    .ToList());
            }
            else
            {
                this.curScreen = curScreen.Next;
                this.HandleCurrentScreenChange();
            }
        }

        public void OnEndEncounter()
        {
            if(caravanManager.HasRemainingMembers())
            {
                this.curScreen = curScreen.Next;
            }
            else
            {
                this.curScreen = gameOverScreen;
            }
            this.PlayAmbient();
            this.HandleCurrentScreenChange();
        }

        private ActorBase choice;

        public void OnRewardPick(ActorBase target)
        {
            caravanManager.AddMember(target);
            this.curScreen = curScreen.Next;
            this.HandleCurrentScreenChange();
        }

        public void OnReplacePick(ActorBase target)
        {
            // Caravan member was swapped out.
        }

        private void CreateRewardScreen()
        {
            Instantiate(GameAssets.Instance.pfRewardScreen, this.transform);
        }

        private void CreateSwapScreen()
        {
            Instantiate(GameAssets.Instance.pfSwapScreen, this.transform);
        }

        private void HandleCurrentScreenChange()
        {
            if (this.curScreen == null)
            {
                SceneManager.LoadScene("TitleMain");
            }

            switch (curScreen)
            {
                case ScriptableObjects.StoryScreen val:
                    {
                        var obj = Instantiate(GameAssets.Instance.pfStoryScreen, this.transform);
                        var screen = obj.GetComponent<Screens.StoryScreen>();
                        screen.Set(val.Title, val.Description, "Continue");
                        screen.scenario = this;
                        PlayAmbient();
                    }
                    break;
                case ScriptableObjects.EncounterScreen val:
                    {
                        var obj = Instantiate(GameAssets.Instance.pfStoryScreen, this.transform);
                        var screen = obj.GetComponent<Screens.StoryScreen>();
                        screen.Set(val.Title, val.Description, "Fight!");
                        screen.scenario = this;
                    }
                    break;
                case ScriptableObjects.RewardScreen val:
                    {
                        var obj = Instantiate(GameAssets.Instance.pfRewardScreen, this.transform);
                        var screen = obj.GetComponentsInChildren<Screens.RewardChoice>();
                        int index = 0;
                        foreach(var member in val.member)
                        {
                            screen[index].Set(obj, this, member);
                            index++;
                        }
                        PlayAmbient();
                    }
                    break;
            }
        }

        void PlayAmbient()
        {
            if (audioSource.clip != ambientAudio)
            {
                audioSource.clip = ambientAudio;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        void PlayEncounter()
        {
            if (audioSource.clip != encounterAudio)
            {
                audioSource.clip = encounterAudio;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }
}
