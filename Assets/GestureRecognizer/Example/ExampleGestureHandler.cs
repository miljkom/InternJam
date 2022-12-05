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
			textResult.text = "Good job";
		}
		else
		{
			textResult.text = "?";
		}
	}
}

