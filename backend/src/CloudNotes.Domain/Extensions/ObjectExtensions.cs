using CloudNotes.Domain.Exceptions.Common;

namespace CloudNotes.Domain.Extensions;

public static class ObjectExtensions
{
    /// <summary>
    /// Se um valor estiver presente, retorna o valor, caso contrário, lança uma exceção informada no parametro.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">objeto</param>
    /// <param name="exception">Classe de exceção que extende BaseException</param>
    /// <returns>o valor, se estiver presente</returns>
    public static T OrElseThrows<T>(this T entity, BaseException exception)
    {
        if (entity is null) throw exception;
        return entity;
    }

    /// <summary>
    /// Se um valor estiver presente, retorna o valor, caso contrário, lança uma exceção informada no parametro.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="task">objeto</param>
    /// <param name="exception">Classe de exceção que extende BaseException</param>
    /// <returns>o valor, se estiver presente</returns>
    public static async Task<T> OrElseThrowsAsync<T>(this Task<T> task, BaseException exception)
    {
        var entity = await task;
        if (entity is null) throw exception;
        return entity;
    }
}
