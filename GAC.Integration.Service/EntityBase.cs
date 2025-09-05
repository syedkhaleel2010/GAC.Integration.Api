// Decompiled with JetBrains decompiler
// Type: MG.EntityBase
// Assembly: MG.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 66FDE214-4281-4361-9B87-64093B08B51B
// Assembly location: C:\Projects\ADMT\admt_licenseandpermits\API\MG.ADMaritime.PermitsAndLicenses.API\bin\Debug\netcoreapp3.1\MG.Core.dll

using GAC.Integration.Service.Interfaces;
using System;

namespace GAC.Integration.Service
{
  public class EntityBase : IEntityBase, IEntityBase<string>
  {
    public virtual string Id { get; set; }

    public virtual DateTime CreatedDate { get; set; }

    public virtual string CreatedBy { get; set; }

    public virtual DateTime? UpdatedDate { get; set; }

    public virtual string UpdatedBy { get; set; }
  }
}
