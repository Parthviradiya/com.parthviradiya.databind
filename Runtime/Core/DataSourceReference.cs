using System;
using UnityEngine;

namespace ParthViradiya.Core
{
	/// <summary>
	/// Enables serialization of data source references on specific ViewModels.
	/// Use DataSourceReference objects on your DataBinding components to create serializable bindings.
	/// </summary>
	[Serializable]
	public class DataSourceReference
	{
		[SerializeField] [HideInInspector] UnityEngine.Object viewModel;
		[SerializeField] [HideInInspector] string dataSourceName;

		private UnityEngine.Object cachedViewModel;
		private string cachedDataSourceName;

		private IDataSource source;

		public IDataSource Source
		{
			get
			{
				if (source == null || viewModel != cachedViewModel || dataSourceName != cachedDataSourceName)
				{
					cachedViewModel = viewModel;
					cachedDataSourceName = dataSourceName;
					if (viewModel == null || string.IsNullOrEmpty(dataSourceName))
					{
						source = null;
						return null;
					}
					var field = viewModel.GetType().GetField(dataSourceName);
					if (field != null)
					{
						source = (IDataSource) field.GetValue(viewModel);
					}
				}
				return source;
			}
		}

		
		public bool TryGetValue<T>(out T value)
		{
			value = default;
			var maybeSource = Source;
			if (maybeSource == null) {
				return false;
			}
			value = maybeSource.GetValue<T>();
			return true;

		}
	}
}