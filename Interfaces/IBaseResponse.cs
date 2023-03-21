﻿using WebRunApplication.Enums;

namespace WebRunApplication.Interfaces
{
    public interface IBaseResponse<T>
    {
        string Description { get; }

        StatusCode StatusCode { get; }

        T Data { get; }
    }
}
