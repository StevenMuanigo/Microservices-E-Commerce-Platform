using NotificationService.DTOs;
using NotificationService.Models;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        // In-memory storage for notifications (in a real application, this would be a database)
        private static readonly List<Notification> _notifications = new List<Notification>();
        private static int _nextId = 1;

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await Task.FromResult(_notifications);
        }

        public async Task<Notification> GetNotificationById(int id)
        {
            var notification = _notifications.FirstOrDefault(n => n.Id == id);
            return await Task.FromResult(notification);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserId(string userId)
        {
            var userNotifications = _notifications.Where(n => n.UserId == userId).ToList();
            return await Task.FromResult(userNotifications);
        }

        public async Task<NotificationDto> CreateNotification(CreateNotificationDto notificationDto)
        {
            var notification = new Notification
            {
                Id = _nextId++,
                UserId = notificationDto.UserId,
                Title = notificationDto.Title,
                Message = notificationDto.Message,
                Type = notificationDto.Type,
                CreatedAt = DateTime.UtcNow
            };

            _notifications.Add(notification);

            // In a real application, you would send the notification via email, SMS, or push notification
            // For this example, we'll just log it
            Console.WriteLine($"Notification sent to user {notification.UserId}: {notification.Title} - {notification.Message}");

            return await Task.FromResult(new NotificationDto
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                Type = notification.Type,
                CreatedAt = notification.CreatedAt,
                IsRead = notification.IsRead
            });
        }

        public async Task<bool> MarkAsRead(int id)
        {
            var notification = _notifications.FirstOrDefault(n => n.Id == id);
            if (notification == null)
                return await Task.FromResult(false);

            notification.IsRead = true;
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteNotification(int id)
        {
            var notification = _notifications.FirstOrDefault(n => n.Id == id);
            if (notification == null)
                return await Task.FromResult(false);

            _notifications.Remove(notification);
            return await Task.FromResult(true);
        }
    }
}