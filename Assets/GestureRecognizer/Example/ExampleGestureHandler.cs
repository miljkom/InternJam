using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using System.Linq;

public class ExampleGestureHandler : MonoBehaviour
{

	public Text textResult;

	public Transform referenceRoot;

	GesturePatternDraw[] references;

	void Start()
	{
		references = referenceRoot.GetComponentsInChildren<GesturePatternDraw>();
	}

	void ShowAll()
	{
		for (int i = 0; i < references.Length; i++)
		{
			references[i].gameObject.SetActive(true);
		}
	}

	public void OnRecognize(RecognitionResult result)
	{
		StopAllCoroutines();
		ShowAll();
		if (result != RecognitionResult.Empty)
		{
			if (result.gesture.id == RandomGesture.instance.currentGesture.id)
			{
				if (RandomElement.instance.currentIndex2 == 4)
				{
					RandomElement.instance.tntBomb();
					textResult.text = "Boom u lost progress";
				}
				else
				{
					Elements.instance.elements[RandomElement.instance.currentIndex2]++;
					textResult.text = "Good job!";
				}
			}
			else
			{
				textResult.text = "Try again?";
			}
		}
		else
		{
			textResult.text = "Try again?";
		}
	}
}

