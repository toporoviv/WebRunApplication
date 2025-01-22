namespace WebRunApplication.Services.Interfaces;

// todo: разобраться с сервисом. Вряд ли он нужен
public interface IBaseService<T>
{
    Task<IBaseResponse<T>> Create(T model);

    Task<IBaseResponse<IEnumerable<T>>> GetAll();

    Task<IBaseResponse<bool>> Delete(long id);
}