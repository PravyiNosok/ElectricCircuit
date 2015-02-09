using UnityEngine;
using System.Collections;

public class ElementButton : MonoBehaviour {
	[SerializeField] private ElementType elementType = 0;
	[SerializeField] private float ElValue = 0;
	[SerializeField] private UILabel label;
	[SerializeField] private UISprite image;
    [SerializeField] private bool Snap = true;
    public void Start() {
        ViewManager.Instance.chStyle += ChangeIcon;
        ChangeIcon(ViewManager.Instance.CurrentStyle.ToString());
    }
	public void ChangeIcon(string atlasName){
		if (image == null)
			return;
		UIAtlas newAtlas = Resources.Load<UIAtlas> (string.Format ("Atlases/{0}", atlasName));
        if (newAtlas != null)
        {
            image.atlas = newAtlas;
            if (Snap) image.MakePixelPerfect();
        }
	}
	public void OnClick(){
		ElementManager.Instance.AddNewElement(elementType,ElValue);
	}
}
