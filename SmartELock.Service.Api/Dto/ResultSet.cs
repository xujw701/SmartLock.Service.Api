using System.Collections.Generic;

namespace SmartELock.Service.Api.Dto
{
    public class ResultSet<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}