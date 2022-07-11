using System;
using ParthViradiya.Core;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


namespace ParthViradiya.Bindings
{
    public class TextBinding : DataBinding
    {
        public TextType textType;
        public Text TextField;
        public TMP_Text TMPField;
        
        [SerializeField][Tooltip("Example = Title : {0}")] string format = string.Empty;
        [AllowedDataTypes(typeof(string))] public DataSourceReference StringSource;

        protected override void Setup()
        {
            Bind(StringSource, OnValueChanged);
        }

        private void OnValueChanged(object sender, EventArgs eventArgs) {
            if (StringSource != null && StringSource.TryGetValue<string>(out var value)) {
                if (TextField) TextField.text = string.IsNullOrEmpty(format) ? value.ToString() : string.Format(format, value);
                if (TMPField) TMPField.text = string.IsNullOrEmpty(format) ? value.ToString() : string.Format(format, value);
            }
        }
    }
}