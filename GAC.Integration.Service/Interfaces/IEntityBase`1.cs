// Decompiled with JetBrains decompiler
// Type: MG.IEntityBase`1
// Assembly: MG.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 66FDE214-4281-4361-9B87-64093B08B51B
// Assembly location: C:\Projects\ADMT\admt_licenseandpermits\API\MG.ADMaritime.PermitsAndLicenses.API\bin\Debug\netcoreapp3.1\MG.Core.dll

using System;

namespace GAC.Integration.Service.Interfaces
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }

        DateTime CreatedDate { get; set; }

        string CreatedBy { get; set; }

        DateTime? UpdatedDate { get; set; }

        string UpdatedBy { get; set; }
    }
}
