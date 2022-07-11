using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParthViradiya.Core;

namespace ParthViradiya.Project
{
	[CreateAssetMenu(fileName = "ViewModel", menuName = "SViewModel")]
	public class AppViewModelScriptable : ViewModelScriptable
	{
		public StringDataSource AppTitle;
		public BoolDataSource active;
		public ColorDataSource color;
		public SpriteDataSource boy;
		public SpriteDataSource girl;
		public IteamDataList list;
	}
}