namespace LotteryService.Common.Enums
{
    public enum LoginResultType
    {
        Success = 1,
        InvalidUserNameAccount = 2,
        InvalidPassword = 3,
        UserIsNotActive = 4,
        UserAlreadyLogged = 5,
        UserEmailIsNotConfirmed = 6,
        UserPhoneIsNotConfirmed = 7,
        UnknownExternalLogin = 8,
        LockedOut = 9
    }
}