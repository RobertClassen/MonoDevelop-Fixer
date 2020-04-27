namespace RCDev.Postprocessors.XML
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using UnityEngine;

	internal class UTF8StringWriter : StringWriter
	{
		#region Fields

		#endregion

		#region Properties
		public override Encoding Encoding
		{ 
			get
			{
				return Encoding.UTF8;
			}
		}
		#endregion

		#region Constructors

		#endregion

		#region Methods

		#endregion
	}
}