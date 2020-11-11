using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Swappable : MonoBehaviour
    {
        public GameController controller;

        public void OnMouseDown()
        {
            var parentView = GetComponentInParent<CaravanMember>();
            controller.SwapWithActive(parentView);
        }
    }
}
