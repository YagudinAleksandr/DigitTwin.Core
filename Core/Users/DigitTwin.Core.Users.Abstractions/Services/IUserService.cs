﻿using DigitTwin.Lib.Abstractions;
using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Существует ли пользователь с таким E-mail
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns>true - существует, false - не существует</returns>
        Task<IBaseApiResponse> IsEmailExists(string email);

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="userCreateDto">Модель создания пользователя <see cref="UserCreateDto"/></param>
        /// <returns>Ответ сервера</returns>
        Task<IBaseApiResponse> Create(UserCreateDto userCreateDto);

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">ИД пользователя</param>
        /// <returns>Ответ сервера</returns>
        Task<IBaseApiResponse> Delete(Guid id);

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="userDto">Пользователь</param>
        /// <returns>Ответ сервера</returns>
        Task<IBaseApiResponse> Update(UserDto userDto);

        /// <summary>
        /// Получение пользователя по ИД
        /// </summary>
        /// <param name="id">ИД пользователя</param>
        /// <returns>Ответ сервера</returns>
        Task<IBaseApiResponse> GetById(Guid id);

        /// <summary>
        /// Получение всех записей
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="maxElements"></param>
        /// <param name="startPosition"></param>
        /// <param name="endPosition"></param>
        /// <returns>Ответ от сервера</returns>
        Task<IBaseApiResponse> GetAllByFilter(Filter filter, int maxElements, int startPosition, int endPosition);
    }
}
