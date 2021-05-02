﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticatorPro.Shared.Data.Backup.Converter
{
    public class UriListBackupConverter : BackupConverter
    {
        public override BackupPasswordPolicy PasswordPolicy => BackupPasswordPolicy.Never;

        public UriListBackupConverter(IIconResolver iconResolver) : base(iconResolver) { }

        public override Task<Backup> Convert(byte[] data, string password = null)
        {
            var text = Encoding.UTF8.GetString(data);
            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var authenticators = new List<Authenticator>();
            authenticators.AddRange(lines.Select(u => Authenticator.FromOtpAuthUri(u, _iconResolver)));

            return Task.FromResult(new Backup(authenticators));
        }
    }
}