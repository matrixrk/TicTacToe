using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

	void Awake()
	{
		Instance = this;
	}

	[SerializeField] Menu[] arraymenu;
	public void OpenMenu(string menuName)
	{

		for (int i = 0; i < arraymenu.Length; i++)
		{
			if (arraymenu[i].menuName == menuName)
			{
				arraymenu[i].Open();
			}
			else if (arraymenu[i].open)
			{
				CloseMenu(arraymenu[i]);
			}
		}
	}

	public void OpenMenu(Menu menu)
	{
		for (int i = 0; i < arraymenu.Length; i++)
		{
			if (arraymenu[i].open)
			{
				CloseMenu(arraymenu[i]);
			}
		}
		menu.Open();
	}
	public void CloseMenu(Menu menu)
	{
		menu.Close();

	}
}
