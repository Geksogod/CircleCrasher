using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Core.UI.Counters
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        protected void SetValue(float value)
        {
            _text.text = ((int)value).ToString(CultureInfo.InstalledUICulture);
        }

        public virtual void UpdateValue()
        {
            throw new NotImplementedException();
        }
    }
}