using System;

namespace EventManagementSystem.APIs.Errors;

[Serializable] // Добавлено для поддержки сериализации
public class NotFoundException : Exception
{
    public int ErrorCode { get; } // Свойство для кода ошибки

    public NotFoundException()
        : base("The requested resource was not found.") // Сообщение по умолчанию
    { }

    public NotFoundException(string message)
        : base(message) { }

    public NotFoundException(string message, Exception inner)
        : base(message, inner) { }

    public NotFoundException(string message, int errorCode)
        : base(message)
    {
        ErrorCode = errorCode; // Установка кода ошибки
    }
}
