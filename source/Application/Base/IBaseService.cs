using DotNetCore.Objects;

namespace DotNetCoreArchitecture.Application
{
    public interface IBaseService
    {
        ErrorDataResult<T> ErrorDataResult<T>(string message);

        ErrorResult ErrorResult(string message);

        SuccessDataResult<T> SuccessDataResult<T>(T data);

        SuccessResult SuccessResult();
    }
}
