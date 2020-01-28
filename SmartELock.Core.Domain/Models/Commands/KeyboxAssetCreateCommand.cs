using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class KeyboxAssetCreateCommand : IKeyboxAssetCommand
    {
        public string Uuid { get; set; }
    }
}
