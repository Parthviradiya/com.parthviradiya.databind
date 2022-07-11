using System;
using ParthViradiya.Core;
using UnityEngine;

namespace ParthViradiya.Project
{
    public class IteamViewModel : ViewModel, IViewModelConfigurable
    {

        public SpriteDataSource _sprite;
        public StringDataSource _name;
        public StringDataSource _price;

        public void Configure(object data)
        {
            if (!(data is IteamData itemData)) return;
            _sprite.SetValue(itemData.iteamImage);
            _name.SetValue(itemData.itemName);
            _price.SetValue(itemData.itemPrice);
        }
    }

    [Serializable]
    public class IteamDataList : DataSourceList<IteamData>
    {
    }

    [Serializable]
    public struct IteamData
    {
        public Sprite iteamImage;
        public string itemName;
        public string itemPrice;
    }
}