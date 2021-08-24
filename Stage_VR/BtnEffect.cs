using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;
using System;

public class BtnEffect : MonoBehaviour
    {
        public Tutorial tutorial;
        Color prevColor;

        private void Start() {
            prevColor = GetComponent<Renderer>().material.color;
        }
        
        public void OnButtonDown(Hand fromHand)
        {
            tutorial.btn = transform;
            Debug.Log(tutorial.btn.name);
            fromHand.TriggerHapticPulse(1000);
        }

        public void OnButtonUp(Hand fromHand)
        {
            tutorial.btn = null;
            StartCoroutine(ColorSelf(Color.white));
        }

        IEnumerator ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();

            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }

            yield return new WaitForSeconds(2f);

            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = prevColor;
            }
        }
    }