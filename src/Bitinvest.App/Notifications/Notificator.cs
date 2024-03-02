
using FluentValidation.Results;

namespace Bitinvest.App.Notifications
{
    public interface INotificator
    {
        bool HasNotification();
        ValidationResult GetNotifications();
        void AddNotification(string notification);
    }
    public class Notificator : INotificator
    {
        private readonly ValidationResult _notifications;
        public Notificator() => _notifications = new ValidationResult();
        public ValidationResult GetNotifications() => _notifications;
        public void AddNotification(string notification) =>
            _notifications.Errors.Add(new ValidationFailure(string.Empty, notification));
        public bool HasNotification() => _notifications.Errors.Any();

    }
}
