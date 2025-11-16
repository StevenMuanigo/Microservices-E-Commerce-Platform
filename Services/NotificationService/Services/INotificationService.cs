using NotificationService.DTOs;
using NotificationService.Models;

namespace NotificationService.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllNotifications();
        Task<Notification> GetNotificationById(int id);
        Task<IEnumerable<Notification>> GetNotificationsByUserId(string userId);
        Task<NotificationDto> CreateNotification(CreateNotificationDto notificationDto);
        Task<bool> MarkAsRead(int id);
        Task<bool> DeleteNotification(int id);
    }
}