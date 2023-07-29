using System;

using MediatR.CommandQuery.Cosmos.Tests.Data.Entities;

namespace MediatR.CommandQuery.Cosmos.Tests.Constants;

public static class UserConstants
{
    ///<summary>William Adama</summary>
    public static readonly User WilliamAdama = new() { Id = "607a27dde412d2a66dd558fb", EmailAddress = "william.adama@battlestar.com", DisplayName = "William Adama", IsEmailAddressConfirmed = true };
    ///<summary>Laura Roslin</summary>
    public static readonly User LauraRoslin = new() { Id = "607a27dde412d2a66dd558fb", EmailAddress = "laura.roslin@battlestar.com", DisplayName = "Laura Roslin", IsEmailAddressConfirmed = true };
    ///<summary>Kara Thrace</summary>
    public static readonly User KaraThrace = new() { Id = "607a27dde412d2a66dd558fb", EmailAddress = "kara.thrace@battlestar.com", DisplayName = "Kara Thrace", IsEmailAddressConfirmed = true };
    ///<summary>Lee Adama</summary>
    public static readonly User LeeAdama = new() { Id = "607a27dde412d2a66dd558fb", EmailAddress = "lee.adama@battlestar.com", DisplayName = "Lee Adama", IsEmailAddressConfirmed = true };
    ///<summary>Gaius Baltar</summary>
    public static readonly User GaiusBaltar = new() { Id = "607a27dde412d2a66dd558fb", EmailAddress = "gaius.baltar@battlestar.com", DisplayName = "Gaius Baltar", IsEmailAddressConfirmed = true };
    ///<summary>Saul Tigh</summary>
    public static readonly User SaulTigh = new() { Id = "607a27dde412d2a66dd558fb", EmailAddress = "saul.tigh@battlestar.com", DisplayName = "Saul Tigh", IsEmailAddressConfirmed = true };
}
