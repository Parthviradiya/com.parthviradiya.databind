using UnityEngine;
using ParthViradiya.Core;

namespace ParthViradiya.Project
{
	public class UiManager : ViewModel
	{
		public StringDataSource title;

		[Header("Data Source Examples")] public SpriteDataSource BoySprite;
		public SpriteDataSource GirlSprite;
		public BoolDataSource IconActive;
		public FloatDataSource IconWidth;
	}
}
