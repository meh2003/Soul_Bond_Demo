using UnityEngine;
using UnityEngine.UI;

namespace Demo
{
    public class HoverButton : MonoBehaviour
    {
        [SerializeField] private Sprite normalImage;
        [SerializeField] private Sprite hoverImage;  
        public static void changeImage(Image buttonHolder, Sprite image) => buttonHolder.sprite = image ;

        public void OnEnter()
        {
            changeImage(gameObject.GetComponent<Image>(), hoverImage);
        }
        
        public void OnExit()
        {
            changeImage(gameObject.GetComponent<Image>(), normalImage);
        }
    }
}