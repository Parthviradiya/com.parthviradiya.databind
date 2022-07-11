using UnityEngine;

namespace ParthViradiya.Core
{
	/// <summary>
	/// ViewModels represent bindable containers of DataSource objects.
	/// All bindings will target a specific ViewModel in order to obtain a list of bindable data sources.
	/// </summary>
	[System.Serializable]public abstract class ViewModel : MonoBehaviour { }

	[System.Serializable]public abstract class ViewModelScriptable : ScriptableObject { }
}