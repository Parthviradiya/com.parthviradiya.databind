using System;
using ParthViradiya.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ParthViradiya.Bindings
{
	public class ImageBinding : DataBinding
	{
		public ImageTarget imageTarget;

		public Image Image;
		[AllowedDataTypes(typeof(Sprite))] public DataSourceReference SpriteSource;
		[AllowedDataTypes(typeof(Color))] public DataSourceReference ColorSource;
		[AllowedDataTypes(typeof(float), typeof(int), typeof(double))] public DataSourceReference WidthSource;
		[AllowedDataTypes(typeof(float), typeof(int), typeof(double))] public DataSourceReference HeightSource;

		protected override void Setup()
		{
			Image = Image ? Image : GetComponent<Image>();
			Bind(SpriteSource, UpdateImage);

			Bind(WidthSource, OnSizeChanged);
			Bind(HeightSource, OnSizeChanged);

			Bind(ColorSource, OnColorChnaged);
		}

		private void UpdateImage(object sender, EventArgs eventArgs)
		{
			Image.sprite = ((DataSource<Sprite>) SpriteSource.Source).Value;
		}

		private void OnSizeChanged(object sender, EventArgs e) {
			if (!Image) return;

			var size = Image.rectTransform.sizeDelta;
			if (WidthSource.TryGetValue<float>(out var width)) {
				size.x = width;
			}
			if (HeightSource.TryGetValue<float>(out var height)) {
				size.y = height;
			}
			Image.rectTransform.sizeDelta = size;
		}

		void OnColorChnaged(object sender,EventArgs eventArgs) {
			if (!Image) return;
			if(ColorSource.TryGetValue(out Color color)) {
				Image.color = color;
            }
			
        }
	}
}