namespace Postprocessors
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;
	using UnityEngine.Serialization;

	[CreateAssetMenu]
	[Serializable]
	internal class Property : ScriptableObject
	{
		#region Fields
		[FormerlySerializedAs("properties")]
		[SerializeField]
		private List<Value> values = new List<Value>();
		[SerializeField]
		private bool isOverwriteEnabled = true;
		[SerializeField]
		private int selectedIndex = 0;
		#endregion

		#region Properties
		public bool IsOverwriteEnabled
		{
			get
			{
				return isOverwriteEnabled;
			}
		}

		public string Value
		{
			get
			{
				return values[selectedIndex].Name;
			}
		}
		#endregion

		#region Constructor

		#endregion

		#region Methods
		public void Draw()
		{
			EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
			for(int i = 0; i < values.Count; i++)
			{
				using(new EditorGUILayout.HorizontalScope())
				{
					using(new EditorGUI.DisabledScope(selectedIndex == i))
					{
						if(GUILayout.Button(values[i].Name, GUILayout.Width(75f)))
						{
							selectedIndex = i;
						}
					}

					EditorGUILayout.LabelField(values[i].Description);
				}
			}
		}
		#endregion
	}
}