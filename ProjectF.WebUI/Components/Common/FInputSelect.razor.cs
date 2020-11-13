using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace ProjectF.WebUI.Components.Common
{
    // Inherit from InputBase so the hard work is already implemented 😊
    public sealed class FInputSelect<TValue> : InputBase<TValue>
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string CustomCssClass { get; set;}
        string _textInputCss = "block appearance-none w-full border border-gray-400 round py-2 px-3 text-gray-700 leading-tight shadow-sm focus:outline-none focus:shadow-outline";
        // Generate html when the component is rendered.
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "select");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", CssClass == "invalid"
                    ? $"{_textInputCss } border border-red-500"
                    : $"{CssClass} {_textInputCss} {CustomCssClass}");
            builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValueAsString));
            builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string>(this, value => CurrentValueAsString = value, CurrentValueAsString, null));
            builder.AddContent(5, ChildContent);
            builder.CloseElement();
        }

        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            if (typeof(TValue) == typeof(string))
            {
                result = (TValue)(object)value;
                validationErrorMessage = null;

                return true;
            }
            else if (typeof(TValue) == typeof(int))
            {
                int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedValue);
                result = (TValue)(object)parsedValue;
                validationErrorMessage = null;

                return true;
            }
            else if (typeof(TValue) == typeof(long))
            {
                long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedValue);
                result = (TValue)(object)parsedValue;
                validationErrorMessage = null;

                return true;
            }
            else if (typeof(TValue) == typeof(Guid))
            {
                Guid.TryParse(value, out var parsedValue);
                result = (TValue)(object)parsedValue;
                validationErrorMessage = null;

                return true;
            }
            else if (typeof(TValue).IsEnum)
            {
                try
                {
                    result = (TValue)Enum.Parse(typeof(TValue), value);
                    validationErrorMessage = null;

                    return true;
                }
                catch (ArgumentException)
                {
                    result = default;
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";

                    return false;
                }
            }

            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(TValue)}'.");
        }

    }
}
