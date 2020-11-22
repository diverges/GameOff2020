using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ActorAnimations : MonoBehaviour
    {
        public Animator animations;
        public Transform combatTextCenter;
        private Queue<(int amount, bool isHeal)> combatTextQueue = new Queue<(int amount, bool isHeal)>();
        private bool run = false;

        public void Flicker()
        {
            StartCoroutine(FlickerSprite());
        }

        public void EnterRight()
        {
            animations.Play("EnterFromRight");
        }

        public void EnterLeft()
        {
            animations.Play("EnterFromLeft");
        }

        public void PlayDamage(int amount)
        {
            animations.Play("Flicker");
            combatTextQueue.Enqueue((amount, false));
            if(!run)
            {
                StartCoroutine(PlayCombatText());
            }
        }

        public void PlayHeal(int amount)
        {
            combatTextQueue.Enqueue((amount, true));
            if (!run)
            {
                StartCoroutine(PlayCombatText());
            }
        }

        private IEnumerator PlayCombatText()
        {
            run = true;
            while(combatTextQueue.Any())
            {
                var item = combatTextQueue.Dequeue();
                CombatText.Create(combatTextCenter, item.amount, item.isHeal);
                yield return new WaitForSeconds(0.3f);
            }
            run = false;
        }

        private IEnumerator FlickerSprite()
        {
            yield return new WaitForSeconds(0.1f);

        }
    }
}
