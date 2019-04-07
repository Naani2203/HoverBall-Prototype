using UnityEngine;


   public class TintMaterial:MonoBehaviour
    {
        [SerializeField]
        private Renderer[] _Emmisives;

        [SerializeField]
        private Renderer[] _ToTint;

        public void ApplyTintToMaterials(Color color, float emmision)
        {
            foreach (var item in _ToTint)
            {
            if(item!=null)
                item.material.color = color;
            }
            foreach (var item in _Emmisives)
            {
                if (item != null)
                {
                    item.materials[1].color = color;
                    item.materials[1].SetColor("_EmissionColor", color * Mathf.LinearToGammaSpace(emmision));
                }

            }
        }

    }
