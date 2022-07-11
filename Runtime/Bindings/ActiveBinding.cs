using System;
using ParthViradiya.Core;
using UnityEngine;

namespace ParthViradiya.Bindings
{
	public class ActiveBinding : DataBinding
	{
		public GameObject Target;
		[AllowedDataTypes(typeof(bool))] public DataSourceReference BoolSource;

		protected override void Setup()
		{
			UnbindOnDisable = false;
			Bind(BoolSource, OnValueChanged);
		}

		private void OnValueChanged(object sender, EventArgs eventArgs)
		{
			if (BoolSource != null && BoolSource.TryGetValue<bool>(out var value))
			{
				if (Target) Target.SetActive(value);
			}
		}
	}
}