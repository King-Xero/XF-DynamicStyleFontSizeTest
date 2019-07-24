using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ButtonsTestApp.Resources.FontScaling
{
    public class FontScaleController : IControlFontScale
    {
        private double _currentScaleFactor = 1;

        public void IncreaseFontScaling()
        {
            double scaleFactor = _currentScaleFactor + 0.2;

            var resources = Application.Current.Resources;

            ApplyScale(resources, scaleFactor);

            _currentScaleFactor = scaleFactor;
        }

        public void DecreaseFontScaling()
        {
            double scaleFactor = _currentScaleFactor - 0.2;

            var resources = Application.Current.Resources;

            ApplyScale(resources, scaleFactor);

            _currentScaleFactor = scaleFactor;
        }

        private void ApplyScale(ResourceDictionary dictionary, double scaleFactor)
        {
            foreach (var resourceDictionary in dictionary.MergedDictionaries)
            {
                ApplyScale(resourceDictionary, scaleFactor);
            }

            Dictionary<string, Style> stylesToAdd = new Dictionary<string, Style>();

            foreach (KeyValuePair<string, object> resource in dictionary)
            {
                if (resource.Value is Style style)
                {
                    foreach (var styleSetter in style.Setters)
                    {
                        if (styleSetter.Property.PropertyName == nameof(Font.FontSize))
                        {
                            if (styleSetter.Value is OnPlatform<int> platformSetter)
                            {
                                foreach (var platform in platformSetter.Platforms)
                                {
                                    var onPlatformFontSize = int.Parse(platform.Value.ToString());
                                    styleSetter.Value = (int) Math.Round(onPlatformFontSize * scaleFactor);
                                }
                            }
                            else
                            {
                                var fontSize = int.Parse(styleSetter.Value.ToString());
                                styleSetter.Value = (int)Math.Round(fontSize * scaleFactor);
                            }
                            stylesToAdd.Add(resource.Key, style);
                        }
                    }
                }
            }

            foreach (var style in stylesToAdd)
            {
                dictionary[style.Key] = style.Value;
            }
        }
    }
}