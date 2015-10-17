using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	public string title;
	public string info;
	public Color backgroundColor = new Color(1f,1f,1f,1f);
	public Color titleFontColor = new Color(0f,0f,0f,1f);
	public Color infoFontColor = new Color(0f,0f,0f,1f);
	public float delay = 0.75f;
	float timer;
	GameObject tooltip;
	GameObject UICanvas;

	private IEnumerator coroutine;
	
	public void OnPointerEnter (PointerEventData eventData) 
	{
		//activate the Tooltip
		tooltip = GameObject.FindWithTag("UI").transform.FindChild("Popups").transform.FindChild("Tooltip").gameObject;

		coroutine = Activate(tooltip);
		StartCoroutine(coroutine);

		Debug.Log(Screen.width);
		Debug.Log(tooltip.GetComponent<RectTransform>().sizeDelta.x);
		float sizingFactor = Screen.width/800f;
		tooltip.GetComponent<Image>().color = backgroundColor;

		tooltip.transform.FindChild("name").gameObject.GetComponent<Text>().text = title;
		tooltip.transform.FindChild("name").gameObject.GetComponent<Text>().color = titleFontColor;
		tooltip.transform.FindChild("info").gameObject.GetComponent<Text>().text = info;
		tooltip.transform.FindChild("info").gameObject.GetComponent<Text>().color = infoFontColor;

		if(info == "")
		{
			float panelWidth = title.Length * 9f;
			tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(panelWidth, 25f);
			tooltip.transform.FindChild("info").GetComponent<RectTransform>().sizeDelta = new Vector2(0f,0f);
			
		}
		else{
			float panelSize = (Mathf.RoundToInt(info.Length / 35))*15;
			tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, panelSize + 40f);
			tooltip.transform.FindChild("info").GetComponent<RectTransform>().sizeDelta = new Vector2(180f, tooltip.transform.FindChild("info").GetComponent<RectTransform>().sizeDelta.y);
			
		}

		tooltip.transform.position = new Vector3(Mathf.Clamp(eventData.position.x,0f,Screen.width-tooltip.GetComponent<RectTransform>().sizeDelta.x*sizingFactor),eventData.position.y);
	


	}
	public void OnPointerExit (PointerEventData eventData) 
	{
		tooltip.SetActive(false);
		StopCoroutine(coroutine);
	
	}
	IEnumerator Activate(GameObject toActivate)
	{
		yield return new WaitForSeconds(delay);
		toActivate.SetActive(true);
	}


}
