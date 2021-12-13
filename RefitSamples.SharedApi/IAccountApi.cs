using Refit;
using RefitSamples.Models;
using System;
namespace RefitSamples.SharedApi
{
    public interface IAccountApi
    {

        [Post("/api/Account/Register")]
        IObservable<AuthResult> Register( UserRegistrationInput input);

        [Headers("Accept: application/json")]
        [Post("/api/Account/Login")]
        IObservable<AuthResult> CreateToken(UserLoginInput input);
    }
}
