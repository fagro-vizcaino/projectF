using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ProjectF.WebUI.Components.Common
{
    public class FAlertNotificationComponent : ComponentBase
    {
        [Inject] public AlertService AlertService { get; set;}
        protected override void OnInitialized()
        {
            Console.WriteLine("AlertComponent:Initializied");
            AlertService.RefreshRequested += Refresh;
        }

        private void Refresh()
        {
            Console.WriteLine("AlertComponent:refreshing");
            StateHasChanged();
        }
    }
    public interface IAlert
    {
        string message { get; set; }
        Alerts alert { get; set; }
    }

    public class Alert : IAlert
    {
        public Alert() { }
        public Alert(string message, Alerts alert)
        {
            this.message = message;
            this.alert = alert;
        }

        public string message { get; set; }
        public Alerts alert { get; set; }
    }

    public enum Alerts
    {
        Info,
        Success,
        Error
    }

    public class AlertService
    {
        public List<IAlert> messages { get; private set; }
        public event Action RefreshRequested;

        public AlertService()
        {
            this.messages = new List<IAlert>();
        }

        public void addMessage(Alert alert)
        {
            this.messages.Add(alert);
            System.Console.WriteLine("Message count: {0}", this.messages.Count);
            RefreshRequested?.Invoke();

            // pop message off after a delay
            new System.Threading.Timer((_) =>
            {
                this.messages.RemoveAt(0);
                RefreshRequested?.Invoke();
            }, null, 8000, System.Threading.Timeout.Infinite);
        }
    }
}
