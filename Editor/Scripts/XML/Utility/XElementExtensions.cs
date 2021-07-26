namespace MonoDevelopFixer.XML
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml;
	using System.Xml.Linq;
	using UnityEngine;

	internal static class XElementExtensions
	{
		#region Fields

		#endregion

		#region Properties

		#endregion

		#region Constructors

		#endregion

		#region Methods
		/// <summary>
		/// Recursively expands XElements if their names and Attributes matches the passed ElementDefinition. 
		/// Applies the SelectedValueOption to leaves.
		/// </summary>
		public static void SetValueRecursively(this XElement xElement, ElementDefinition elementDefinition, int depth = 0)
		{
			foreach(XElement child in xElement.Elements())
			{
				if(child.Name.LocalName != elementDefinition.Elements[depth].Name)
				{
					return;
				}

				XAttribute[] xAttributes = child.Attributes().ToArray();
				Element.Attribute[] attributes = elementDefinition.Elements[depth].Attributes;
				if(xAttributes.Length != attributes.Length)
				{
					return;
				}

				foreach(Element.Attribute attribute in attributes)
				{
					if(!xAttributes.Any(xAttribute => 
						xAttribute.Name.LocalName == attribute.Name && xAttribute.Value == attribute.Value))
					{
						return;
					}
				}

				if(depth < elementDefinition.Elements.Length - 1)
				{
					if(child.NodeType == XmlNodeType.Element && child.HasElements)
					{
						SetValueRecursively(child, elementDefinition, depth + 1);
					}
				}
				else
				{
					child.Value = elementDefinition.SelectedValueOption;
				}
			}
		}
		#endregion
	}
}