using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Components.Common
{
    public class ValidationMessageBase<TValue> : ComponentBase, IDisposable
    {
        private FieldIdentifier _fieldIdentifier;

        [CascadingParameter] private EditContext EditContext { get; set; }
        [Parameter] public Expression<Func<TValue>> For { get; set; }
        [Parameter] public string Class { get; set; }

        protected IEnumerable<string> ValidationMessages => EditContext.GetValidationMessages(_fieldIdentifier);

        protected override void OnInitialized()
        {
            _fieldIdentifier = FieldIdentifier.Create(For);
            EditContext.OnValidationStateChanged += HandleValidationStateChanged;
        }
 
        private void HandleValidationStateChanged(object o, ValidationStateChangedEventArgs args) 
            => StateHasChanged();

        public void Dispose()
        {
            EditContext.OnValidationStateChanged -= HandleValidationStateChanged;
        }
    }
}
