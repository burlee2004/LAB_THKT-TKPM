using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASC.Tests.TestUtilities
{
    public class FakeSession : ISession
    {
        public bool IsAvailable => true; // Giả lập luôn có sẵn session
        public string Id { get; } = Guid.NewGuid().ToString();
        public IEnumerable<string> Keys => sessionFactory.Keys;

        private Dictionary<string, byte[]> sessionFactory = new Dictionary<string, byte[]>();

        public void Clear()
        {
            sessionFactory.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask; // Không làm gì cả, chỉ trả về Task hoàn thành
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask; // Không làm gì cả, chỉ trả về Task hoàn thành
        }

        public void Remove(string key)
        {
            sessionFactory.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            sessionFactory[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            return sessionFactory.TryGetValue(key, out value);
        }







    }
}
